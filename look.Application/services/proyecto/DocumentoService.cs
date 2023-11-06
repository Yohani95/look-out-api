using look.Application.interfaces.proyecto;
using look.Application.services.Common;
using look.domain.entities.Common;
using look.domain.entities.proyecto;
using look.domain.interfaces;
using look.domain.interfaces.proyecto;
using look.domain.interfaces.unitOfWork;
using Microsoft.AspNetCore.Http;
using Serilog;


namespace look.Application.services.proyecto
{
    public class DocumentoService : Service<Documento>, IDocumentoService
    {
        //instanciar repository si se requiere 
        private readonly IDocumentoRepository _documentoRepository;
        private readonly ILogger _logger = Logger.GetLogger();
        private readonly IUnitOfWork _unitOfWork;

        public DocumentoService(IDocumentoRepository documentoRepository,IUnitOfWork unitOfWork) : base(documentoRepository)
        {
            _documentoRepository = documentoRepository;
            _unitOfWork= unitOfWork;
        }

        public async  Task<ServiceResult> SendDocuments(List<IFormFile> files,int idProyecto,int idCliente)
        {
            List<Documento> documentos=new List<Documento>();
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                foreach (var file in files)
                {
                    var url= await FileServices.UploadFileAsync(file, idCliente, idProyecto);
                    if (url.Equals("") )
                        return new ServiceResult { IsSuccess = false, Message = Message.SinDocumentos, MessageCode = ServiceResultMessage.InvalidInput };
                    var documento = await _documentoRepository.AddAsync(new Documento
                    {
                        DocExtencion = System.IO.Path.GetExtension(file.FileName),
                        DocNombre = file.FileName,
                        DocUrl = url.ToString(),
                        DocIdCliente = idCliente,
                        TdoId = 1
                    });
                    documentos.Add(documento);
                    
                }
                await _unitOfWork.CommitAsync();
                return new ServiceResult
                {
                    MessageCode = ServiceResultMessage.Success,
                    IsSuccess = true,
                    Message = Message.PeticionOk,
                };
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                documentos.ForEach(documento => { FileServices.DeleteFile(documento.DocUrl); });
                _logger.Information("Error al crear el proyecto: " + ex.Message);
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = $"Error interno del servidor: {ex.Message}",
                    MessageCode = ServiceResultMessage.InternalServerError
                };
            }
            
        }
    }
}
