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

namespace Aplicación.Logica.Libro
{
    public class Insertar
    {
        public class EjecutaLibro: IRequest<Unit>
        {
            public string nombre { get; set; }
            public int paginas { get; set; }
            public string descripcion { get; set; }
            public int edicion { get; set; }
            public DateTime fecha_publicacion { get; set; }
            public Guid autor_id { get; set; }
            public Guid categoria_id { get; set; }
            public Guid editorial_id { get; set; }
        }

        public class Validador: AbstractValidator<EjecutaLibro>
        {
            public Validador()
            {
                RuleFor(x => x.nombre).NotEmpty();
                RuleFor(x => x.paginas).NotEmpty();
                RuleFor(x => x.descripcion).NotEmpty();
                RuleFor(x => x.edicion).NotEmpty();
                RuleFor(x => x.fecha_publicacion).NotEmpty();
                RuleFor(x => x.autor_id).NotEmpty();
                RuleFor(x => x.categoria_id).NotEmpty();
                RuleFor(x => x.editorial_id).NotEmpty();
                
            }
        }

        public class Manejador : IRequestHandler<EjecutaLibro, Unit>
        {
            private readonly ProyectoContext _context;
            public Manejador(ProyectoContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(EjecutaLibro request, CancellationToken cancellationToken)
            {
                var libro = new Libros { 
                id = Guid.NewGuid(),
                nombre = request.nombre,
                paginas = request.paginas,
                descripcion = request.descripcion,
                edicion = request.edicion,
                fecha_publicacion = request.fecha_publicacion,
                autor_id = request.autor_id,
                categoria_id = request.categoria_id,
                editorial_id = request.editorial_id,
                
                };

                _context.Libros.Add(libro);

                var resultado = await _context.SaveChangesAsync();
                if (resultado > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo insertar el libro");
            }
        }
    }
}
