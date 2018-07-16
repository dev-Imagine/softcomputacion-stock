using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using softcomputacion.Models;

namespace softcomputacion.Servicios
{
    public class srvEstado
    {
        public List<estado> ObtenerEstados()
        {
            try
            {
                using (BDSoftComputacionEntities bd = new BDSoftComputacionEntities())
                {
                    return bd.estado.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}