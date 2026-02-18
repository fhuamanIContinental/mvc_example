using DbModel.demoDb;
using DtoModel.Persona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Repository.PersonaRepo.Mapping
{
    public static class PersonaMapping
    {

        public static PersonaDto ToDto(this Persona persona)
        {
            return new PersonaDto
            {
                Id = persona.Id,
                IdTipoDocumento = persona.IdTipoDocumento,
                Nombres = persona.Nombres,
                ApellidoPaterno = persona.ApellidoPaterno,
                ApellidoMaterno = persona.ApellidoMaterno,
                Direccion = persona.Direccion,
                Telefono = persona.Telefono,
                UserCreate = persona.UserCreate,
                UserUpdate = persona.UserUpdate,
                DateCreated = persona.DateCreated,
                DateUpdate = persona.DateUpdate
            };
        }
         public static Persona ToEntity(this PersonaDto personaDto)
        {
            return new Persona
            {
                Id = personaDto.Id,
                IdTipoDocumento = personaDto.IdTipoDocumento,
                Nombres = personaDto.Nombres,
                ApellidoPaterno = personaDto.ApellidoPaterno,
                ApellidoMaterno = personaDto.ApellidoMaterno,
                Direccion = personaDto.Direccion,
                Telefono = personaDto.Telefono,
                UserCreate = personaDto.UserCreate,
                UserUpdate = personaDto.UserUpdate,
                DateCreated = personaDto.DateCreated,
                DateUpdate = personaDto.DateUpdate
            };
        }

        public static List<PersonaDto> ToDtoList(this List<Persona> personas)
        {
            return personas.Select(p => p.ToDto()).ToList();
        }
         public static List<Persona> ToEntityList(this List<PersonaDto> personaDtos)
        {
            return personaDtos.Select(p => p.ToEntity()).ToList();
        }


    }
}
