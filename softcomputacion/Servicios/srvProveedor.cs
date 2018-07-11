using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using softcomputacion.Models;

namespace softcomputacion.Servicios
{
    public class srvProveedor
    {
        private proveedor GuardarProveedor(proveedor oProveedor)
        {
            try
            {
                using (BDSoftComputacionEntities bd = new BDSoftComputacionEntities())
                {
                    oProveedor.nombreEmpresa = oProveedor.nombreEmpresa.ToUpper();
                    bd.Entry(oProveedor).State = System.Data.Entity.EntityState.Added;
                    bd.SaveChanges();
                    return oProveedor;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private proveedor ModificarProveedor(proveedor oProveedor)
        {
            try
            {
                using (BDSoftComputacionEntities bd = new BDSoftComputacionEntities())
                {
                    oProveedor.nombreEmpresa = oProveedor.nombreEmpresa.ToUpper();
                    oProveedor.proveedorXproducto.Clear();
                    bd.Entry(oProveedor).State = System.Data.Entity.EntityState.Modified;
                    bd.SaveChanges();
                    return oProveedor;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public proveedor GuardarModificarProveedor(proveedor oProveedor)
        {
            try
            {
                if (oProveedor.idProveedor==0)
                {
                    GuardarProveedor(oProveedor);
                }
                else
                {
                    ModificarProveedor(oProveedor);
                }
                return oProveedor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<proveedor> ObtenerProveedores()
        {
            try
            {
                using (BDSoftComputacionEntities bd = new BDSoftComputacionEntities())
                {
                    return bd.proveedor.OrderBy(x => x.nombreEmpresa).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<proveedor> ObtenerProveedores(string nombreEmpresa)
        {
            try
            {
                using (BDSoftComputacionEntities bd = new BDSoftComputacionEntities())
                {
                    if (nombreEmpresa == "")
                    {
                        return bd.proveedor.ToList();
                    }
                    return bd.proveedor.Where(x => x.nombreEmpresa.Contains(nombreEmpresa.ToUpper())).OrderBy(x => x.nombreEmpresa).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}