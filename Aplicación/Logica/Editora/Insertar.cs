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

namespace Aplicación.Logica.Editora
{
    public class Insertar
    {
        public class EjecutaEditorial: IRequest<Unit>
        {
            public string nombre { get; set; }
        }

        public class Validador: AbstractValidator<EjecutaEditorial>
        {
            public Validador()
            {
                RuleFor(x => x.nombre).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<EjecutaEditorial, Unit>
        {
            private readonly ProyectoContext _context;
            public Manejador(ProyectoContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(EjecutaEditorial request, CancellationToken cancellationToken)
            {
                var editora = new Editorial { 
                    id = Guid.NewGuid(),
                    nombre = request.nombre,
                
                };
                _context.Editorial.Add(editora);
                var resultado = await _context.SaveChangesAsync();


                if (resultado > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo insertar la editorial");
            }
        }
    }
}
