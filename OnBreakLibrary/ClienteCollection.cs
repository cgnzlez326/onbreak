using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnbreakDALC;

namespace OnBreakLibrary
{
    [Serializable]
    public class ClienteCollection
    {
        private List<Cliente> _clientes = new List<Cliente>();

        OnbreakDBEntities db = new OnbreakDBEntities();


        public List<ActividadEmpresa> ListarActividadEmpresa()
        {
            List<ActividadEmpresa> actividades = (from ae in db.ACTIVIDADEMPRESAS
                                                  select new ActividadEmpresa
                                                  {
                                                      Id = ae.IdActividadEmpresa,
                                                      DescripcionActividad = ae.Descripcion
                                                  }).ToList();

            return actividades;
        }


        public List<Cliente> ListarClientes()
        {
            List<Cliente> clientes = (from cli in db.CLIENTES
                                      select new Cliente
                                      {
                                          Rut = cli.RutCliente,
                                          Razon_social = cli.RazonSocial,
                                          Direccion = cli.Direccion,
                                          Nombre_contacto = cli.NombreContacto,
                                          Email_contacto = cli.MailContacto,
                                          Telefono = cli.Telefono,
                                          ActividadEmpresaCliente = new ActividadEmpresa
                                          {
                                              Id = cli.IdActividadEmpresa,
                                              DescripcionActividad = cli.ACTIVIDADEMPRESAS.Descripcion
                                          },
                                          TipoEmpresaCliente = new TipoEmpresa
                                          {
                                              Id = cli.IdTipoEmpresa,
                                              DescripcionTipoEmpresa = cli.TIPOEMPRESAS.Descripcion
                                          }
                                      }).ToList();
            return clientes;
        }

        public List<TipoEmpresa> ListarTipoEmpresa()
        {
            List<TipoEmpresa> tipos = (from te in db.TIPOEMPRESAS
                                       select new TipoEmpresa
                                       {
                                           Id = te.IdTipoEmpresa,
                                           DescripcionTipoEmpresa = te.Descripcion
                                       }).ToList();
            return tipos;
        }

        public List<Cliente> Clientes
        {
            get
            {
                return _clientes;
            }

            set
            {
                _clientes = value;
            }
        }

        public bool GuardarClientes(Cliente cliente)
        {
            Cliente cliente1 = BuscarCliente(cliente.Rut);

            if (cliente1 == null)
            {
                try
                {
                    OnbreakDALC.CLIENTES c = new CLIENTES();
                    c.RutCliente = cliente.Rut;
                    c.RazonSocial = cliente.Razon_social;
                    c.Direccion = cliente.Direccion;
                    c.NombreContacto = cliente.Nombre_contacto;
                    c.MailContacto = cliente.Email_contacto;
                    c.Telefono = cliente.Telefono;
                    c.IdActividadEmpresa = cliente.ActividadEmpresaCliente.Id;
                    c.IdTipoEmpresa = cliente.TipoEmpresaCliente.Id;

                    db.CLIENTES.Add(c);
                    db.SaveChanges();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;

        }

        public Cliente BuscarCliente(string rut)
        {
            Cliente cliente = (from c in db.CLIENTES
                               where c.RutCliente.StartsWith(rut)
                               select new Cliente
                               {
                                   Rut = c.RutCliente,
                                   Nombre_contacto = c.NombreContacto,
                                   Direccion = c.Direccion,
                                   Email_contacto = c.MailContacto,
                                   Telefono = c.Telefono,
                                   Razon_social = c.RazonSocial,
                                   ActividadEmpresaCliente = new ActividadEmpresa
                                   {
                                       Id = c.ACTIVIDADEMPRESAS.IdActividadEmpresa,
                                       DescripcionActividad = c.ACTIVIDADEMPRESAS.Descripcion
                                   },
                                   TipoEmpresaCliente = new TipoEmpresa
                                   {
                                       Id = c.TIPOEMPRESAS.IdTipoEmpresa,
                                       DescripcionTipoEmpresa = c.TIPOEMPRESAS.Descripcion
                                   }
                               }).FirstOrDefault();
            return cliente;
        }

        public bool ModificarCliente(Cliente cliente)
        {
            try
            {
                OnbreakDALC.CLIENTES c = (from cli in db.CLIENTES where cli.RutCliente == cliente.Rut select cli).FirstOrDefault();

                if (c == null)
                {
                    return false;
                }

                c.RazonSocial = cliente.Razon_social;
                c.Direccion = cliente.Direccion;
                c.NombreContacto = cliente.Nombre_contacto;
                c.MailContacto = cliente.Email_contacto;
                c.Telefono = cliente.Telefono;
                c.IdActividadEmpresa = cliente.ActividadEmpresaCliente.Id;
                c.IdTipoEmpresa = cliente.TipoEmpresaCliente.Id;

                db.Entry(c).State = System.Data.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool EliminarCliente(string rut)
        {
            OnbreakDALC.CLIENTES cliente = (from c in db.CLIENTES where c.RutCliente == rut select c).FirstOrDefault();

            if (cliente == null)
            {
                return false;
            }
            try
            {
                db.CLIENTES.Remove(cliente);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public List<Cliente> BuscarClientePorRubro(int idActividadEmpresa)
        {
            List<Cliente> ClientesPorRubro = (from c in db.CLIENTES
                                              where c.IdActividadEmpresa == idActividadEmpresa
                                              select new Cliente
                                              {
                                                  Rut = c.RutCliente,
                                                  Nombre_contacto = c.NombreContacto,
                                                  Direccion = c.Direccion,
                                                  Email_contacto = c.MailContacto,
                                                  Telefono = c.Telefono,
                                                  Razon_social = c.RazonSocial,
                                                  ActividadEmpresaCliente = new ActividadEmpresa
                                                  {
                                                      Id = c.ACTIVIDADEMPRESAS.IdActividadEmpresa,
                                                      DescripcionActividad = c.ACTIVIDADEMPRESAS.Descripcion
                                                  },
                                                  TipoEmpresaCliente = new TipoEmpresa
                                                  {
                                                      Id = c.TIPOEMPRESAS.IdTipoEmpresa,
                                                      DescripcionTipoEmpresa = c.TIPOEMPRESAS.Descripcion
                                                  }
                                              }).ToList();
            return ClientesPorRubro;
        }

        public List<Cliente> BuscarClientePorRut(string rut)
        {
            List<Cliente> ClientesPorRut = (from c in db.CLIENTES
                                            where c.RutCliente.StartsWith(rut)
                                            select new Cliente
                                            {
                                                Rut = c.RutCliente,
                                                Nombre_contacto = c.NombreContacto,
                                                Direccion = c.Direccion,
                                                Email_contacto = c.MailContacto,
                                                Telefono = c.Telefono,
                                                Razon_social = c.RazonSocial,
                                                ActividadEmpresaCliente = new ActividadEmpresa
                                                {
                                                    Id = c.ACTIVIDADEMPRESAS.IdActividadEmpresa,
                                                    DescripcionActividad = c.ACTIVIDADEMPRESAS.Descripcion
                                                },
                                                TipoEmpresaCliente = new TipoEmpresa
                                                {
                                                    Id = c.TIPOEMPRESAS.IdTipoEmpresa,
                                                    DescripcionTipoEmpresa = c.TIPOEMPRESAS.Descripcion
                                                }
                                            }).ToList();
            return ClientesPorRut;
        }

        public List<Cliente> BuscarClientePorTipoEmpresa(int idTipoEmpresa)
        {
            List<Cliente> ClientePorTipo = (from c in db.CLIENTES
                                            where c.IdTipoEmpresa == idTipoEmpresa
                                            select new Cliente
                                            {
                                                Rut = c.RutCliente,
                                                Nombre_contacto = c.NombreContacto,
                                                Direccion = c.Direccion,
                                                Email_contacto = c.MailContacto,
                                                Telefono = c.Telefono,
                                                Razon_social = c.RazonSocial,
                                                ActividadEmpresaCliente = new ActividadEmpresa
                                                {
                                                    Id = c.ACTIVIDADEMPRESAS.IdActividadEmpresa,
                                                    DescripcionActividad = c.ACTIVIDADEMPRESAS.Descripcion
                                                },
                                                TipoEmpresaCliente = new TipoEmpresa
                                                {
                                                    Id = c.TIPOEMPRESAS.IdTipoEmpresa,
                                                    DescripcionTipoEmpresa = c.TIPOEMPRESAS.Descripcion
                                                }
                                            }).ToList();
            return ClientePorTipo;
        }
    }
}
