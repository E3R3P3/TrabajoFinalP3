using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using FluentValidation;
using Accesodatos.Tablas;
using Accesodatos.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Aplicación.Logica.Categoria
{
    public class Insertar
    {
        public class Ejecuta: IRequest<Unit>
        {
            public string Nombre { get; set; }
        }

        public class Validador: AbstractValidator<Ejecuta>
        {
            public Validador()
            {

                RuleFor(x => x.Nombre).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta, Unit>
        {
            private readonly ProyectoContext _context;
            public Manejador(ProyectoContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var categoria = new Categorias
                {
                    id = Guid.NewGuid(),
                    nombre = request.Nombre
                };
                _context.Categorias.Add(categoria);
                var resultado = await _context.SaveChangesAsync();

                if (resultado > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo insertar la categoria");

            }
        }
    }
}
