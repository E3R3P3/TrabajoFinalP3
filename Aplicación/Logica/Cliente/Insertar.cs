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

namespace Aplicación.Logica.Cliente
{
    public class Insertar
    {
       public class EjecutaCliente: IRequest<Unit>
        {
            public string nombre { get; set; }
            public string apellido { get; set; }
            public string correo { get; set; }
            public int matricula { get; set; }
            //public string? foto_perfil { get; set; }
        }

        public class Validador: AbstractValidator<EjecutaCliente>
        {
            public Validador()
            {
                RuleFor(x => x.nombre).NotEmpty();
                RuleFor(x => x.apellido).NotEmpty();
                RuleFor(x =>x.correo).NotEmpty();
                RuleFor(x => x.matricula).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<EjecutaCliente, Unit>
        {
            private readonly ProyectoContext _context;
            public Manejador(ProyectoContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(EjecutaCliente request, CancellationToken cancellationToken)
            {
                var clientes = new Clientes {

                    id = Guid.NewGuid(),
                    nombre = request.nombre,
                    apellido = request.apellido,
                    correo = request.correo,
                    matricula = request.matricula

                
                };
                _context.Clientes.Add(clientes);
                var resultado = await _context.SaveChangesAsync();

                if (resultado > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo insertar el cliente");
            }
        }
    }
}
