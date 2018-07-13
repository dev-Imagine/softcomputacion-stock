using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using softcomputacion.Models;

namespace softcomputacion.Servicios
{
    public class srvProducto
    {
        private producto GuardarProducto(producto oProducto)
        {
            try
            {
                using (BDSoftComputacionEntities bd = new BDSoftComputacionEntities())
                {
                    oProducto.nombre = oProducto.nombre.ToUpper();
                    bd.Entry(oProducto).State = System.Data.Entity.EntityState.Added;
                    bd.SaveChanges();
                    return oProducto;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private producto ModificarProducto(producto oProducto)
        {
            try
            {
                using (BDSoftComputacionEntities bd = new BDSoftComputacionEntities())
                {
                    oProducto.nombre = oProducto.nombre.ToUpper();
                    bd.Entry(oProducto).State = System.Data.Entity.EntityState.Modified;
                    bd.SaveChanges();
                    return oProducto;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public producto GuardarModificarProducto(producto oProducto)
        {
            try
            {
                if (oProducto.idProducto == 0)
                {
                    GuardarProducto(oProducto);
                }
                else
                {
                    ModificarProducto(oProducto);
                }
                return oProducto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}