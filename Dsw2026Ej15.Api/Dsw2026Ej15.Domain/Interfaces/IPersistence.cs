using System;
using System.Collections.Generic;
using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Domain.Interfaces;

public interface IPersistence
{
    Speciality? GetSpecialityById(Guid Id);
    void AddDoctor(Doctor doctor);
    List<Doctor> GetDoctors();

}
