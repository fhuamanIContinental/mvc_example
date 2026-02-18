using DbModel.demoDb;
using DtoModel.Persona;
using Microsoft.EntityFrameworkCore;
using Mvc.Repository.PersonaRepo.Contratos;
using Mvc.Repository.PersonaRepo.Mapping;

namespace Mvc.Repository.PersonaRepo.Implementacion
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly _demoContext _db;

        //Estamos generando un constructor 
        //este nos permite inicializar cualquier recurso que necesitemos
        //para nuestra clase, como conexiones a bases de datos, servicios externos, etc.
        public PersonaRepository(_demoContext db)
        {
            _db = db;
        }

        public async Task<PersonaDto> Create(PersonaDto request)
        {

            Persona person = PersonaMapping.ToEntity(request);
            await _db.Persona.AddAsync(person);
            await _db.SaveChangesAsync();

            request = PersonaMapping.ToDto(person);
            return request;
        }

        public async Task Delete(int id)
        {
            await _db.Persona.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<List<PersonaDto>> GetAll()
        {
            List<PersonaDto> res = new List<PersonaDto>();
            List<Persona> data = await _db.Persona.ToListAsync();
            res = PersonaMapping.ToDtoList(data);
            return res;
        }

        public async Task<PersonaDto?> GetById(int id)
        {
            Persona? persona = await _db.Persona.Where(x => x.Id == id).FirstOrDefaultAsync();
            if(persona == null) { return null; }
            PersonaDto res = PersonaMapping.ToDto(persona);
            return res;
        }

        public async Task<PersonaDto> Update(PersonaDto request)
        {
            Persona person = PersonaMapping.ToEntity(request);
            _db.Persona.Update(person);
            await _db.SaveChangesAsync();

            request = PersonaMapping.ToDto(person);
            return request;


        }
    }
}


// programación en segundo plano
// programación en hilos