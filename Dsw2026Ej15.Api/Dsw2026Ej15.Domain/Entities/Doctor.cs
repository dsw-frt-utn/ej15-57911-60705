using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Entities;

public class Doctor : BaseEntity
{
    public String Name { get; set; }
    public string LicenseNumber { get; set; } 
    public bool IsActive { get; private set; } 
    public Speciality? Speciality { get; private set; } 

    public Doctor(string name, string licenseNumber, Speciality speciality)
    {
        Name = name;
        LicenseNumber = licenseNumber;
        Speciality = speciality;
        IsActive = true;

    }
}
