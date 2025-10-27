using Security.Models;
using Security.Models.DTOS;
using Security.Repositories;

namespace Security.Services
{
    public class HospitalService : IHospitalService
    {
        private readonly IHospitalRepository _repo;
        public HospitalService(IHospitalRepository repo)
        {
            _repo = repo;
        }
        public async Task<Hospital> CreateHospital(CreateHospitalDto dto)
        {
            var hospital = new Hospital
            {
                Id = dto.Id,
                Name = dto.Name,
                Address = dto.Address,
                Type = dto.Type
            };
            await _repo.Add(hospital);
            return hospital;
        }

        public async Task<IEnumerable<Hospital>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<Hospital> GetOne(Guid id)
        {
            return await _repo.GetOne(id);
        }

       public async Task<bool> DeleteHospital(Guid id)
        {
            var existing = await _repo.GetOne(id);
            if (existing == null) return false;
            await _repo.Delete(id);
            return true;
        }
        public async Task<IEnumerable<Hospital>> GetAllHospitalsType13()
        {
            var hospitals = await _repo.GetAll();
            var filtered = hospitals.Where(h => h.Type == 1 || h.Type == 3);
            return filtered;
        }

        public async Task<bool> UpdateHospital(Guid id, UpdateHospitalDto dto)
        {
            var hospital = await _repo.GetOne(id);
            if (hospital == null) return false;

            hospital.Name = dto.Name ?? hospital.Name;
            hospital.Address = dto.Address ?? hospital.Address;
            hospital.Type = dto.Type ?? hospital.Type;

            await _repo.Update(hospital);
            return true;

        }
        }
    }
