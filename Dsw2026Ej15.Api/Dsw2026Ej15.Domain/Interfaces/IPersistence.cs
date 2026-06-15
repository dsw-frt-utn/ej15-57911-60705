using System;
using System.Collections.Generic;
using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Domain.Interfaces;

public interface IPersistence
{
    // Métodos para Especialidades
    List<Speciality> GetSpecialities();
    Speciality? GetSpecialityById(Guid id);

    // Métodos para Médicos
    List<Doctor> GetDoctors();
    Doctor? GetDoctorById(Guid id);
    void AddDoctor(Doctor doctor);
    void UpdateDoctor(Doctor doctor);
}
