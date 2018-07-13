using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using softcomputacion.Models;

namespace softcomputacion.Servicios
{
    public class srvCategoria
    {
        public List<categoria> ObtenerCategorias()
        {
            try
            {
                using (BDSoftComputacionEntities bd = new BDSoftComputacionEntities())
                {
                    return bd.categoria.OrderBy(x => x.nombre).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<categoria>();
            }
        }
        public categoria ObtenerCategoria(int idCategoria)
        {
            try
            {
                using (BDSoftComputacionEntities bd = new BDSoftComputacionEntities())
                {
                    categoria oCategoria = bd.categoria.Where(x => x.idCategoria == idCategoria).FirstOrDefault();
                    oCategoria.subcategoria.Count();
                    return oCategoria;
                }
            }
            catch (Exception ex)
            {
                return new categoria();
            }
        }
        public categoria GuardarModificarCategoria (categoria oCategoria)
        {
            if (oCategoria.idCategoria ==0)
            {
                oCategoria = GuardarCategoria(oCategoria);
            }
            else
            {
                //oCategoria = ModificarCategoria(oCategoria); //falta actualizar detalle
            }
            return oCategoria;
        }
        private categoria GuardarCategoria(categoria oCategoria)
        {
            try
            {
                using (BDSoftComputacionEntities bd = new BDSoftComputacionEntities())
                {   
                    bd.Entry(oCategoria).State = System.Data.Entity.EntityState.Added;
                    bd.SaveChanges();
                    return oCategoria;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private categoria ModificarCategoria (categoria oCategoria)
        {
            try
            {
                using (BDSoftComputacionEntities bd = new BDSoftComputacionEntities())
                {
                    bd.Entry(oCategoria).State = System.Data.Entity.EntityState.Modified;
                    bd.SaveChanges();
                    return oCategoria;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}