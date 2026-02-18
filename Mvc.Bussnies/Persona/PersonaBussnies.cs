using DtoModel.Persona;
using Mvc.Repository.PersonaRepo.Contratos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mvc.Bussnies.Persona
{
    public class PersonaBussnies : IPersonaBussnies
    {
        private readonly IPersonaRepository _personaRepository;

        public PersonaBussnies(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<PersonaDto> Create(PersonaDto request)
        {
            PersonaDto result = await _personaRepository.Create(request);
            return result;
        }

        public async Task<List<PersonaDto>> GetAll()
        {
            List<PersonaDto> lista = await _personaRepository.GetAll();
            return lista;
        }

        public async Task<PersonaDto?> GetById(int id)
        {
            PersonaDto person = await _personaRepository.GetById(id);

            return person;

        }

        public Task<PersonaDto?> Update(PersonaDto request)
        {
            PersonaDto? personDb = _personaRepository.GetById(request.Id).Result;

            if (personDb == null)
            {
                new  Exception("persona a actualizar no existe");

            }

            personDb.DateUpdate = request.DateUpdate;
            personDb.UserUpdate = request.UserUpdate;

            _personaRepository.Update(personDb);

            return Task.FromResult<PersonaDto?>(personDb);

        }

        public async Task Delete(int id)
        {
            await _personaRepository.Delete(id);
        }
    }
}
