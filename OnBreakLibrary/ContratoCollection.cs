using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OnbreakDALC;


namespace OnBreakLibrary
{
    [Serializable]
    public class ContratoCollection
    {
        private List<Contrato> _contratos = new List<Contrato>();
        OnbreakDBEntities db = new OnbreakDBEntities();

        public List<Contrato> Contratos
        {
            get
            {
                return _contratos;
            }
            set
            {
                _contratos = value;
            }
        }

        public List<Contrato> ListarContratos()
        {

            return (from con in db.CONTRATOS select new Contrato
            {
                NumeroContrato = con.Numero,
                Rut = con.RutCliente,
                NombreEvento = con.NombreEvento,
                ModalidadServicioContrato = new ModalidadServicio
                {
                    Id = con.IdModalidad,
                    IdEventoEmpresa = new TipoEventoEmpresa
                    {
                        Id = con.MODALIDADSERVICIOS.IdTipoEvento,
                        DescripcionEvento = con.MODALIDADSERVICIOS.TIPOEVENTOS.Descripcion
                    },
                    NombreModalidad = con.MODALIDADSERVICIOS.Nombre,
                    ValorBase = con.MODALIDADSERVICIOS.ValorBase,
                    PersonalBase = con.MODALIDADSERVICIOS.PersonalBase,
                },
                Direccion = con.Direccion,
                CantAsist = con.Asistentes,
                PersonalAdicional = con.PersonalAdicional,
                Total = con.ValorTotalContrato,
                InicioEvento = con.FechaHoraInicio,
                TerminoEvento = con.FechaHoraTermino,
                CreacionContrato = (DateTime)con.Creacion,
                TerminoContrato = (DateTime)con.Termino,
                Vigente = con.Realizado,
                Observaciones = con.Observaciones
            }).ToList();
        }

        public List<ModalidadServicio> ListarModalidadServicio()
        {
            List<ModalidadServicio> modalidad = (from ms in db.MODALIDADSERVICIOS
                                                 select new ModalidadServicio
                                                 {
                                                     Id = ms.IdModalidad,
                                                     NombreModalidad = ms.Nombre
                                                 }).ToList();
            return modalidad;
        }

        public List<TipoEventoEmpresa> ListarTipoEvento()
        {
            List<TipoEventoEmpresa> tipoevento = (from te in db.TIPOEVENTOS
                                                  select new TipoEventoEmpresa
                                                  {
                                                      Id = te.IdTipoEvento,
                                                      DescripcionEvento = te.Descripcion
                                                  }).ToList();
            return tipoevento;
        }

        public List<ModalidadServicio> ListarModalidadPorTipoEvento(int tipoeventoid)
        {
            List<ModalidadServicio> servicios = (from ms in db.MODALIDADSERVICIOS
                                                 select new ModalidadServicio
                                                 {
                                                     Id = ms.IdModalidad,
                                                     IdEventoEmpresa = new TipoEventoEmpresa
                                                     {
                                                         Id = ms.IdTipoEvento,
                                                         DescripcionEvento = ms.TIPOEVENTOS.Descripcion
                                                     },
                                                     NombreModalidad = ms.Nombre,
                                                     ValorBase = ms.ValorBase,
                                                     PersonalBase = ms.PersonalBase
                                                 }).ToList();

            List<ModalidadServicio> query = (from servicio in servicios
                         where servicio.IdEventoEmpresa.Id == tipoeventoid
                         select servicio).ToList();

            

            return query;
        }

        public Contrato BuscarContrato(string nroContrato)
        {
            Contrato contrato = (from con in db.CONTRATOS
                                 where con.Numero == nroContrato
                                 select new Contrato
                                 {
                                     NumeroContrato = con.Numero,
                                     Rut = con.RutCliente,
                                     NombreEvento = con.NombreEvento,
                                     ModalidadServicioContrato = new ModalidadServicio
                                     {
                                         Id = con.IdModalidad,
                                         IdEventoEmpresa = new TipoEventoEmpresa
                                         {
                                             Id = con.MODALIDADSERVICIOS.IdTipoEvento,
                                             DescripcionEvento = con.MODALIDADSERVICIOS.TIPOEVENTOS.Descripcion
                                         },
                                         NombreModalidad = con.MODALIDADSERVICIOS.Nombre,
                                         ValorBase = con.MODALIDADSERVICIOS.ValorBase,
                                         PersonalBase = con.MODALIDADSERVICIOS.PersonalBase,
                                     },
                                     Direccion = con.Direccion,
                                     CantAsist = con.Asistentes,
                                     PersonalAdicional = con.PersonalAdicional,
                                     Total = con.ValorTotalContrato,
                                     InicioEvento = con.FechaHoraInicio,
                                     TerminoEvento = con.FechaHoraTermino,
                                     CreacionContrato = (DateTime)con.Creacion,
                                     TerminoContrato = (DateTime)con.Termino,
                                     Vigente = con.Realizado,
                                     Observaciones = con.Observaciones
                                 }).FirstOrDefault();
            return contrato;
        }

        public List<Contrato> BuscarContratosPorTipo(int tipoEvento)
        {
            List<Contrato> ContratosPorRubro = ((from con in db.CONTRATOS
                                                 where con.MODALIDADSERVICIOS.IdTipoEvento == tipoEvento
                                                 select new Contrato
                                                 {
                                                     NumeroContrato = con.Numero,
                                                     Rut = con.RutCliente,
                                                     NombreEvento = con.NombreEvento,
                                                     ModalidadServicioContrato = new ModalidadServicio
                                                     {
                                                         Id = con.IdModalidad,
                                                         IdEventoEmpresa = new TipoEventoEmpresa
                                                         {
                                                             Id = con.MODALIDADSERVICIOS.IdTipoEvento,
                                                             DescripcionEvento = con.MODALIDADSERVICIOS.TIPOEVENTOS.Descripcion
                                                         },
                                                         NombreModalidad = con.MODALIDADSERVICIOS.Nombre,
                                                         ValorBase = con.MODALIDADSERVICIOS.ValorBase,
                                                         PersonalBase = con.MODALIDADSERVICIOS.PersonalBase,
                                                     },
                                                     Direccion = con.Direccion,
                                                     CantAsist = con.Asistentes,
                                                     PersonalAdicional = con.PersonalAdicional,
                                                     Total = con.ValorTotalContrato,
                                                     InicioEvento = con.FechaHoraInicio,
                                                     TerminoEvento = con.FechaHoraTermino,
                                                     CreacionContrato = (DateTime)con.Creacion,
                                                     TerminoContrato = (DateTime)con.Termino,
                                                     Vigente = con.Realizado,
                                                     Observaciones = con.Observaciones
                                                 })).ToList();
            return ContratosPorRubro;
        }

        public List<Contrato> BuscarContratosPorRut(string rut)
        {
            List<Contrato> ContratosPorRut = ((from con in db.CONTRATOS
                                               where con.RutCliente.StartsWith(rut)
                                               select new Contrato
                                               {
                                                   NumeroContrato = con.Numero,
                                                   Rut = con.RutCliente,
                                                   NombreEvento = con.NombreEvento,
                                                   ModalidadServicioContrato = new ModalidadServicio
                                                   {
                                                       Id = con.IdModalidad,
                                                       IdEventoEmpresa = new TipoEventoEmpresa
                                                       {
                                                           Id = con.MODALIDADSERVICIOS.IdTipoEvento,
                                                           DescripcionEvento = con.MODALIDADSERVICIOS.TIPOEVENTOS.Descripcion
                                                       },
                                                       NombreModalidad = con.MODALIDADSERVICIOS.Nombre,
                                                       ValorBase = con.MODALIDADSERVICIOS.ValorBase,
                                                       PersonalBase = con.MODALIDADSERVICIOS.PersonalBase,
                                                   },
                                                   Direccion = con.Direccion,
                                                   CantAsist = con.Asistentes,
                                                   PersonalAdicional = con.PersonalAdicional,
                                                   Total = con.ValorTotalContrato,
                                                   InicioEvento = con.FechaHoraInicio,
                                                   TerminoEvento = con.FechaHoraTermino,
                                                   CreacionContrato = (DateTime)con.Creacion,
                                                   TerminoContrato = (DateTime)con.Termino,
                                                   Vigente = con.Realizado,
                                                   Observaciones = con.Observaciones
                                               })).ToList();
            return ContratosPorRut;
        }

        public bool GuardarContrato(Contrato contrato)
        {
            Contrato contrato1 = BuscarContrato(contrato.NumeroContrato);

        if (contrato1 == null)
            {
                try
                {
                    OnbreakDALC.CONTRATOS con = new CONTRATOS();

                    con.Numero = contrato.NumeroContrato;
                    con.RutCliente = contrato.Rut;
                    con.NombreEvento = contrato.NombreEvento;
                    con.Creacion = contrato.CreacionContrato;
                    con.Termino = contrato.TerminoContrato;
                    con.Direccion = contrato.Direccion;
                    con.IdModalidad = contrato.ModalidadServicioContrato.Id;
                    con.FechaHoraInicio = contrato.InicioEvento;
                    con.FechaHoraTermino = contrato.TerminoEvento;
                    con.Asistentes = contrato.CantAsist;
                    con.PersonalAdicional = contrato.PersonalAdicional;
                    con.Realizado = contrato.Vigente;
                    con.PrecioBase = contrato.ModalidadServicioContrato.ValorBase;
                    con.ValorTotalContrato = contrato.Total;

                    db.CONTRATOS.Add(con);
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TerminarContrato(Contrato contrato)
        {

            if (contrato != null)
            {
                try
                {
                    OnbreakDALC.CONTRATOS con = (from contratos in db.CONTRATOS where contrato.NumeroContrato == contratos.Numero select contratos).FirstOrDefault();
                    con.Termino = contrato.TerminoContrato;
                    con.Realizado = contrato.Vigente;

                    db.Entry(con).State = System.Data.EntityState.Modified;
                    db.SaveChanges();

                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Contrato> BuscarContratoPorNumeroContrato(string nrocontrato)
        {
            List<Contrato> ContratosPorNumero = ((from con in db.CONTRATOS
                                                  where con.Numero.StartsWith(nrocontrato)
                                                  select new Contrato
                                                  {
                                                      NumeroContrato = con.Numero,
                                                      Rut = con.RutCliente,
                                                      NombreEvento = con.NombreEvento,
                                                      ModalidadServicioContrato = new ModalidadServicio
                                                      {
                                                          Id = con.IdModalidad,
                                                          IdEventoEmpresa = new TipoEventoEmpresa
                                                          {
                                                              Id = con.MODALIDADSERVICIOS.IdTipoEvento,
                                                              DescripcionEvento = con.MODALIDADSERVICIOS.TIPOEVENTOS.Descripcion
                                                          },
                                                          NombreModalidad = con.MODALIDADSERVICIOS.Nombre,
                                                          ValorBase = con.MODALIDADSERVICIOS.ValorBase,
                                                          PersonalBase = con.MODALIDADSERVICIOS.PersonalBase,
                                                      },
                                                      Direccion = con.Direccion,
                                                      CantAsist = con.Asistentes,
                                                      PersonalAdicional = con.PersonalAdicional,
                                                      Total = con.ValorTotalContrato,
                                                      InicioEvento = con.FechaHoraInicio,
                                                      TerminoEvento = con.FechaHoraTermino,
                                                      CreacionContrato = (DateTime)con.Creacion,
                                                      TerminoContrato = (DateTime)con.Termino,
                                                      Vigente = con.Realizado,
                                                      Observaciones = con.Observaciones
                                                  })).ToList();
            return ContratosPorNumero;
        }


        public ModalidadServicio BuscarValoresPorModalidad(string modalidadId)
        {
            ModalidadServicio modalidad = (from ms in db.MODALIDADSERVICIOS
                                           where modalidadId == ms.IdModalidad
                                           select new ModalidadServicio
                                           {
                                               Id = ms.IdModalidad,
                                               IdEventoEmpresa = new TipoEventoEmpresa
                                               {
                                                   Id = ms.IdTipoEvento,
                                                   DescripcionEvento = ms.TIPOEVENTOS.Descripcion
                                               },
                                               NombreModalidad = ms.Nombre,
                                               ValorBase = ms.ValorBase,
                                               PersonalBase = ms.PersonalBase
                                           }).FirstOrDefault();

            return modalidad;
        }

    }
}
