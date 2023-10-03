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


        public ProyectoService(IProyectoRepository proyectoRepository, IPropuestaRepository propuestaRepository, IDocumentoService documentoService, IProyectoDocumentoService proyectoDocumentoService,IUnitOfWork unitOfWork) : base(proyectoRepository)
        {
            _proyectoRepository = proyectoRepository;
            _propuestaRepository = propuestaRepository;         
            _documentoService = documentoService;
            _proyectoDocumentoService = proyectoDocumentoService;
            _unitOfWork= unitOfWork;

        }

        public async Task<ServiceResult> createAsync(IFormFile file1, IFormFile file2, Proyecto proyecto)
        {
            try
            {
                _logger.Information("Crear proyecto");
                await _unitOfWork.BeginTransactionAsync();
                if (proyecto == null)
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        Message = Message.EntidadNull,
                        MessageCode = ServiceResultMessage.InvalidInput
                    };
                }
                var propuesta = new Propuesta
                {
                    MonId=proyecto.MonId,
                    PrpPresupuesto=0,
                    EppId=1,
                    PrpDescripcion="N/A",
                    TseId=1,
                    PrsId=1,
                };

                var propuestaCreated=await _propuestaRepository.AddAsync(propuesta);
                proyecto.PryId = propuestaCreated.PrpId;
                var proyectoCreated=await _proyectoRepository.AddAsync(proyecto);

                var urlArchivo1 = await FileServices.UploadFileAsync(file1, (int)proyecto.PryIdCliente,proyectoCreated.PryId);
                var urlArchivo2 = await FileServices.UploadFileAsync(file2, (int)proyecto.PryIdCliente,proyectoCreated.PryId);
                if (urlArchivo1.Equals("") || urlArchivo2.Equals(""))
                    return new ServiceResult { IsSuccess = false, Message = Message.SinDocumentos, MessageCode = ServiceResultMessage.InvalidInput };
                //completar correctamente segun lo que se requiere con todos los campos

                var documento1 = await _documentoService.AddAsync(new Documento
                {
                    DocExtencion = file1.ContentType,
                    DocNombre = file1.FileName,
                    DocUrl = urlArchivo1.ToString(),
                    DocIdCliente = proyecto.PryIdCliente,
                    TdoId = 1
                });
                var documento2 = await _documentoService.AddAsync(new Documento
                {
                    DocExtencion = file2.ContentType,
                    DocNombre = file2.FileName,
                    DocUrl = urlArchivo2.ToString(),
                    DocIdCliente = proyecto.PryIdCliente,
                    TdoId = 1
                });
                await _proyectoDocumentoService.AddAsync(new ProyectoDocumento{PryId = proyecto.PryId,DocId = documento1.DocId, TdoId = 1 });
                await _proyectoDocumentoService.AddAsync(new ProyectoDocumento{PryId = proyecto.PryId,DocId = documento2.DocId,TdoId=1});

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
                var proyectoDocumentos = await _proyectoDocumentoService.GetAllAsync();
                proyectoDocumentos.Where(d=>d.PryId == id).ToList();

                foreach (var proDoc in proyectoDocumentos)
                {
                    var documento = await _documentoService.GetByIdAsync(proDoc.DocId);
                    bool deleted = FileServices.DeleteFile(documento.DocUrl);

                    if (!deleted)
                    {
                        await _documentoService.DeleteAsync(documento);
                    }
                }

                // Elimina el proyecto después de eliminar la propuesta y los documentos.
                await _proyectoRepository.DeleteAsync(existingProyecto);

                _logger.Information("Proyecto con propuesta y documentos eliminado exitosamente");
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

                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = $"Error interno del servidor: {ex.Message}",
                    MessageCode = ServiceResultMessage.InternalServerError
                };
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

        public async Task<ServiceResult> updateAsync(IFormFile file1, IFormFile file2, Proyecto proyecto, int id)
        {
            try
            {
                _logger.Information("Actualizar proyecto con documentos");
                proyecto.PrpId=id;
                if (proyecto == null || proyecto.PryId == 0)
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        Message = Message.EntidadNull,
                        MessageCode = ServiceResultMessage.InvalidInput
                    };
                }

                // Verifica si el proyecto existe en la base de datos antes de actualizarlo.
                var existingProyecto = await _proyectoRepository.GetByIdAsync(proyecto.PryId);
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
                existingProyecto.MonId = proyecto.MonId;
                existingProyecto.EpyId = proyecto.EpyId;
                existingProyecto.TseId = proyecto.TseId;
                existingProyecto.EsProy= proyecto.EsProy;
                existingProyecto.PrpId= proyecto.PrpId;
                existingProyecto.PryFechaCierre= proyecto.PryFechaCierre;
                existingProyecto.PryFechaInicioEstimada= proyecto.PryFechaCierreEstimada;
                existingProyecto.PryValor= proyecto.PryValor;

                var proyectoDocumentos = await _proyectoDocumentoService.GetAllAsync();
                foreach (var proyectoDocumento in proyectoDocumentos.Where(p => p.PryId == existingProyecto.PryId))
                {
                    var documento = await _documentoService.GetByIdAsync(proyectoDocumento.DocId);
                    FileServices.DeleteFile(documento.DocUrl);
                    await _documentoService.DeleteAsync(documento);
                }

                // Actualiza los documentos si se proporcionan archivos actualizados.
                if (file1 != null)
                {
                    var urlArchivo1 = FileServices.UploadFileAsync(file1, (int)proyecto.PryIdCliente,1);
                    await _documentoService.AddAsync(new Documento
                    {
                        DocExtencion = file1.ContentType,
                        DocNombre = file1.FileName,
                        DocUrl = urlArchivo1.ToString()
                    });
                }

                if (file2 != null)
                {
                    var urlArchivo2 = FileServices.UploadFileAsync(file2, (int)proyecto.PryIdCliente,1);
                    await _documentoService.AddAsync(new Documento
                    {
                        DocExtencion = file2.ContentType,
                        DocNombre = file2.FileName,
                        DocUrl = urlArchivo2.ToString()
                    });
                }
                // Llama al repositorio para guardar los cambios en la base de datos.
                await _proyectoRepository.UpdateAsync(existingProyecto);

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
