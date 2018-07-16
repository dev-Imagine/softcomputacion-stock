﻿using System;
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
                oProducto.nombre = oProducto.nombre.ToUpper();
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

        public List<producto> ObtenerProductos(string nombre = "", int idCategoria = 0, int idSubCategoria=0, int idEstado=0)
        {
            try
            {
                List<producto> lstProductos = new List<producto>();
                nombre = nombre.ToUpper();
                using (BDSoftComputacionEntities bd = new BDSoftComputacionEntities())
                {
                    if (nombre == "") lstProductos = bd.producto.ToList();
                    else lstProductos = bd.producto.Where(x => x.nombre.Contains(nombre)).ToList();
                    if (idCategoria != 0) lstProductos = lstProductos.Where(x => x.idCategoria == idCategoria).ToList();
                    if (idSubCategoria != 0) lstProductos = lstProductos.Where(x => x.idSubCategoria == idSubCategoria).ToList();
                    if (idEstado != 0) lstProductos = lstProductos.Where(x => x.idEstado == idEstado).ToList();
                    foreach (producto opro in lstProductos)
                    {
                        int id;
                        id = opro.categoria.idCategoria;
                        id = opro.subcategoria.idSubCategoria;
                        id = opro.subcategoria.categoria.idCategoria;
                        id = opro.estado.idEstado;
                        id = opro.proveedorXproducto.Count();

                    }
                    return lstProductos;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public producto ObtenerProducto(int idProducto)
        {
            try
            {
                using (BDSoftComputacionEntities bd = new BDSoftComputacionEntities())
                {

                    return bd.producto.Where(X => X.idProducto == idProducto).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}