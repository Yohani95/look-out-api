using look.Application.interfaces.cuentas;
using look.Application.interfaces.proyecto;
using look.Application.interfaces.world;
using look.Application.services.Common;
using look.Application.services.cuentas;
using look.Application.services.world;
using look.domain.dto.admin;
using look.domain.entities.Common;
using look.domain.entities.cuentas;
using look.domain.entities.proyecto;
using look.domain.entities.world;
using look.domain.interfaces;
using look.domain.interfaces.proyecto;
using look.domain.interfaces.unitOfWork;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace look.Application.services.proyecto
{
    public class ProyectoService : Service<Proyecto>, IProyectoService
    {
        //instanciar repository si se requiere 
        private readonly IProyectoRepository _proyectoRepository;
        private readonly ILogger _logger = Logger.GetLogger();
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPropuestaRepository _propuestaRepository;
        private readonly IDocumentoService _documentoService;
        private readonly IProyectoDocumentoService _proyectoDocumentoService;
        private readonly ITarifarioConvenioService _tarifarioConvenioService;


        public ProyectoService(IProyectoRepository proyectoRepository, IPropuestaRepository propuestaRepository, IDocumentoService documentoService, IProyectoDocumentoService proyectoDocumentoService,IUnitOfWork unitOfWork, ITarifarioConvenioService tarifarioConvenioService) : base(proyectoRepository)
        {
            _proyectoRepository = proyectoRepository;
            _propuestaRepository = propuestaRepository;         
            _documentoService = documentoService;
            _proyectoDocumentoService = proyectoDocumentoService;
            _tarifarioConvenioService = tarifarioConvenioService;
            _unitOfWork= unitOfWork;
        }

        public async Task<ServiceResult> createAsync(List<IFormFile> files, ProyectoDTO proyectoDTO)
        {
            List<Documento> documentos=new List<Documento>();
            try
            {           
                _logger.Information("Crear proyecto");
                await _unitOfWork.BeginTransactionAsync();
                if (proyectoDTO == null)
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        Message = Message.EntidadNull,
                        MessageCode = ServiceResultMessage.InvalidInput
                    };
                }
                var propuestaCreated=await _propuestaRepository.AddAsync(new Propuesta{MonId = proyectoDTO.Proyecto.MonId,PrpPresupuesto = 0,EppId = 1,PrpDescripcion = "N/A",TseId = 1,PrsId = 1,});
                proyectoDTO.Proyecto.PrpId= propuestaCreated.PrpId;
                var proyectoCreated=await _proyectoRepository.AddAsync(proyectoDTO.Proyecto);
#region "Guardar Archivos"
                foreach (var file in files)
                {
                    var url= await FileServices.UploadFileAsync(file, (int)proyectoCreated.PryIdCliente, proyectoCreated.PryId);
                    if (url.Equals("") )
                        return new ServiceResult { IsSuccess = false, Message = Message.SinDocumentos, MessageCode = ServiceResultMessage.InvalidInput };
                    var documento = await _documentoService.AddAsync(new Documento
                    {
                        DocExtencion = System.IO.Path.GetExtension(file.FileName),
                        DocNombre = file.FileName,
                        DocUrl = url.ToString(),
                        DocIdCliente = proyectoCreated.PryIdCliente,
                        TdoId = 1
                    });
                    documentos.Add(documento);
                }
                foreach (var doc in documentos)
                {
                    await _proyectoDocumentoService.AddAsync(new ProyectoDocumento { PryId = proyectoCreated.PryId, DocId = doc.DocId, TdoId = 1 });
                }
                #endregion

                await _unitOfWork.CommitAsync();
                _logger.Information("Proyecto creado exitosamente");
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

        public async Task<ServiceResult> deleteAsync(int id)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                _logger.Information("Eliminar proyecto con documentos y propuesta");

                if (id <= 0)
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        Message = Message.EntidadNull,
                        MessageCode = ServiceResultMessage.InvalidInput
                    };
                }

                // Verifica si el proyecto existe en la base de datos antes de eliminarlo.
                var existingProyecto = await _proyectoRepository.GetByIdAsync(id);
                if (existingProyecto == null)
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        Message = Message.EntidadNull,
                        MessageCode = ServiceResultMessage.InvalidInput
                    };
                }

                // Elimina la propuesta asociada al proyecto.
                var propuesta=await _propuestaRepository.GetByIdAsync((int)existingProyecto.PrpId);
                await _propuestaRepository.DeleteAsync(propuesta);

                // Elimina los documentos asociados al proyecto.
                var proyectoDocumentos = await _proyectoDocumentoService.ListComplete();
                var proyectoDocumentoFiltrado =proyectoDocumentos.Where(d=>d.PryId == id).ToList();

                foreach (var proDoc in proyectoDocumentoFiltrado)
                {
                    var documento = await _documentoService.GetByIdAsync(proDoc.DocId);
                    bool deleted = FileServices.DeleteFile(documento.DocUrl);

                    if (!deleted)
                    {
                        await _documentoService.DeleteAsync(documento);
                    }
                }
                // Elimina los participantes asociados al proyecto.
                var tarifarioConvenido = await _tarifarioConvenioService.ListComplete();
                var tarifarioConvenidoFiltrado =tarifarioConvenido.Where(d=>d.PRpId == id).ToList();
                foreach (var tarifariolist in tarifarioConvenidoFiltrado)
                {
                    await _tarifarioConvenioService.DeleteAsync(tarifariolist);
                }
                // Elimina el proyecto después de eliminar la propuesta y los documentos.
                await _proyectoRepository.DeleteAsync(existingProyecto);
                
                _logger.Information("Proyecto con propuesta y documentos eliminado exitosamente");
                await _unitOfWork.CommitAsync();
                return new ServiceResult
                {
                    IsSuccess = true,
                    Message = Message.PeticionOk,
                    MessageCode = ServiceResultMessage.Success
                };
                
            }
            catch (Exception ex)
            {
                _logger.Error("Error al eliminar el proyecto con propuesta y documentos: " + ex.Message);
                await _unitOfWork.RollbackAsync();
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = $"Error interno del servidor: {ex.Message}",
                    MessageCode = ServiceResultMessage.InternalServerError
                };
            }
        }

        public async Task<ResponseGeneric<Proyecto>> GetByIdAllEntities(int id)
        {

            try
            {
                _logger.Information("obteniendo proyecto ID: "+ id);
                if (id <= 0)
                {
                    var result = new ServiceResult
                    {
                        IsSuccess = false,
                        MessageCode = ServiceResultMessage.NotFound,
                        Message = "No hay datos."
                    };
                    return new ResponseGeneric<Proyecto>
                    {
                        serviceResult = result,
                        Data = null
                    };
                }
                var proyecto = await _proyectoRepository.GetComplete();
                var resultSuccess = new ServiceResult
                {
                    IsSuccess = true,
                    MessageCode = ServiceResultMessage.Success,
                    Message = Message.PeticionOk
                };
                return new ResponseGeneric<Proyecto>
                {
                    serviceResult = resultSuccess,
                    Data = proyecto.FirstOrDefault(p=>p.PryId==id)
                };
            }
            catch (Exception ex)
            {
                _logger.Information(Message.ErrorServidor + ex.Message);
                var errorResult = new ServiceResult
                {
                    IsSuccess = false,
                    MessageCode = ServiceResultMessage.InternalServerError,
                    Message =  Message.ErrorServidor+ex.Message
                };

                return new ResponseGeneric<Proyecto>
                {
                    serviceResult = errorResult,
                    Data = null
                };
            }
        }

        public async Task<List<Proyecto>> GetComplete()
        {
            try
            {
                return await _proyectoRepository.GetComplete();
            }
            catch (Exception ex)
            {
                _logger.Error("Error interno del servidor: " + ex.Message);
                return null;
            }
        }

        public async Task<List<FileStream>> GetFile(int idProject)
        {
            try
            {
                /*funcion a revisar ya que aun  nose determina bien 
                 * el funcionamiento o si cumple con lo que se requiere en la logica de 
                negocio*/
                _logger.Information("Descargando archivos para el proyecto");
                var proyectoDocumento = await _proyectoDocumentoService.GetByIdProject(idProject);
                List<FileStream> files = new List<FileStream>();
                foreach (var item in proyectoDocumento)
                {
                    files.Add(FileServices.GetFile(item.Documento.DocUrl));
                }
                return files;
            }
            catch (Exception ex)
            {
                _logger.Error("Error interno del servidor: " + ex.Message);
            }
            return null;
        }

        public async Task<ResponseGeneric<int>> GetLastId()
        {
            try
            {
                var proy = await _proyectoRepository.GetAllAsync();

                if (proy == null)
                {
                    var result = new ServiceResult
                    {
                        IsSuccess = false,
                        MessageCode = ServiceResultMessage.NotFound,
                        Message = "No hay datos."
                    };
                    return new ResponseGeneric<int>
                    {
                        serviceResult = result,
                        Data = 0
                    };
                }

                var id = proy == null ? 0 : proy.LastOrDefault().PryId;

                var resultSuccess = new ServiceResult
                {
                    IsSuccess = true,
                    MessageCode = ServiceResultMessage.Success,
                    Message = Message.PeticionOk
                };

                return new ResponseGeneric<int>
                {
                    serviceResult = resultSuccess,
                    Data = id
                };

            }
            catch (Exception ex)
            {
                var errorResult = new ServiceResult
                {
                    IsSuccess = false,
                    MessageCode = ServiceResultMessage.InternalServerError,
                    Message = $"Error interno del servidor: {ex.Message}"
                };

                return new ResponseGeneric<int>
                {
                    serviceResult = errorResult,
                    Data = 0
                };
            }
        }

        public async Task<ServiceResult> updateAsync(List<IFormFile> files, ProyectoDTO proyectoDTO)
        {
            List<Documento> doc = new List<Documento>();
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                _logger.Information("Actualizar proyecto con documentos");
                if (proyectoDTO == null || proyectoDTO.Proyecto.PryId == 0)
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        Message = Message.EntidadNull,
                        MessageCode = ServiceResultMessage.InvalidInput
                    };
                }

                // Verifica si el proyecto existe en la base de datos antes de actualizarlo.
                var existingProyecto = await _proyectoRepository.GetByIdAsync(proyectoDTO.Proyecto.PryId);
                if (existingProyecto == null)
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        Message = Message.EntidadNull,
                        MessageCode = ServiceResultMessage.InvalidInput
                    };
                }

                // Actualiza los campos del proyecto con los valores proporcionados.
                existingProyecto.MonId = proyectoDTO.Proyecto.MonId;
                existingProyecto.EpyId = proyectoDTO.Proyecto.EpyId;
                existingProyecto.TseId = proyectoDTO.Proyecto.TseId;
                existingProyecto.EsProy= proyectoDTO.Proyecto.EsProy;
                existingProyecto.PrpId= proyectoDTO.Proyecto.PrpId;
                existingProyecto.PryFechaCierre= proyectoDTO.Proyecto.PryFechaCierre;
                existingProyecto.PryFechaInicioEstimada= proyectoDTO.Proyecto.PryFechaInicioEstimada;
                existingProyecto.FechaCorte = proyectoDTO.Proyecto.FechaCorte;
                existingProyecto.PryValor= proyectoDTO.Proyecto.PryValor;
                existingProyecto.FacturacionDiaHabil= proyectoDTO.Proyecto.FacturacionDiaHabil;

                var proyectoDocumentos = await _proyectoDocumentoService.GetAllAsync();
                foreach (var proyectoDocumento in proyectoDocumentos.Where(p => p.PryId == existingProyecto.PryId))
                {
                    var documento = await _documentoService.GetByIdAsync(proyectoDocumento.DocId);
                    IFormFile archivoEncontrado = files.FirstOrDefault(file =>
                    {
                        string nombreArchivo = file.FileName; 
                        return nombreArchivo.Equals(documento.DocNombre, StringComparison.OrdinalIgnoreCase);
                    });

                    if (archivoEncontrado != null)
                    { 
                        using (var reader = new StreamReader(archivoEncontrado.OpenReadStream()))
                        {
                            var fileContent = reader.ReadToEnd();
                            if (!string.IsNullOrWhiteSpace(fileContent))
                            {
                                string pathArchivo = Path.GetFileName(documento.DocUrl);
                                FileServices.UploadFileAsyncEdit(archivoEncontrado,proyectoDTO.Proyecto.PryIdCliente,existingProyecto.PryId,pathArchivo);
                                files.Remove(archivoEncontrado);
                            }
                            else
                            {
                                
                            }
                        }
                        
                    }
                    else
                    {
                        FileServices.DeleteFile(documento.DocUrl);
                        await _documentoService.DeleteAsync(documento);
                        
                    }
                }
                // Actualiza los documentos si se proporcionan archivos actualizados.
                foreach (var documentos in files)
                {
                    var urlArchivo1 = await FileServices.UploadFileAsync(documentos, (int)proyectoDTO.Proyecto.PryIdCliente, proyectoDTO.Proyecto.PryId);
                    var documento = await _documentoService.AddAsync(new Documento
                    {
                        DocExtencion = documentos.ContentType,
                        DocNombre = documentos.FileName,
                        DocUrl = urlArchivo1.ToString(),
                        DocIdCliente = proyectoDTO.Proyecto.PryIdCliente,
                        TdoId = 1
                    });
                    await _proyectoDocumentoService.AddAsync(new ProyectoDocumento{PryId = proyectoDTO.Proyecto.PryId,DocId = documento.DocId, TdoId = 1 });
                }
                // Llama al repositorio para guardar los cambios en la base de datos.
                await _proyectoRepository.UpdateAsync(existingProyecto);
                
                await _unitOfWork.CommitAsync();

                _logger.Information("Proyecto con documentos actualizado exitosamente");
                return new ServiceResult
                {
                    MessageCode = ServiceResultMessage.Success,
                    IsSuccess = true,
                    Message = Message.PeticionOk,
                };
            }
            catch (Exception ex)
            {
                //doc.ForEach(documento => { FileServices.DeleteFile(documento.DocUrl); });
                await _unitOfWork.RollbackAsync();
                _logger.Error("Error al actualizar el proyecto con documentos: " + ex.Message);
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
