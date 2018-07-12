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
    }
}