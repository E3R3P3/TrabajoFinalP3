﻿using Microsoft.AspNetCore.Mvc;
using Accesodatos.Context;
using Accesodatos.Tablas;
using MediatR;
using System.Threading.Tasks;
using System.Collections.Generic;
using Aplicación.Logica.Categoria;


namespace SGBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : GeneralController
    {

        [HttpPost]
        public async Task<ActionResult<Unit>> Insertar(Insertar.Ejecuta datos)
        {
            return await Mediator.Send(datos);
        }

        [HttpGet]
        public async Task<ActionResult<List<Categorias>>> Lista()
        {
            return await Mediator.Send(new Consulta.ListaCategorias());
        }
    }
}