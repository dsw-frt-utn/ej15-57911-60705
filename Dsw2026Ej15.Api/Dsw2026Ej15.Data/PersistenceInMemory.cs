using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;

namespace Dsw2026Ej15.Data;

public class PersistenceInMemory : IPersistence
{
    private readonly List<Speciality> _specialities = new();
    private readonly List<Doctor> _doctors = new();

    public PersistenceInMemory()
    {
        LoadSpecialities();
    }
    private void LoadSpecialities()
    {
        try
        {
            
            string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "specialities.json");

            if (File.Exists(rutaArchivo))
            {
                string jsonString = File.ReadAllText(rutaArchivo);

                var opciones = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var listaCargada = JsonSerializer.Deserialize<List<Speciality>>(jsonString, opciones);

                if (listaCargada != null)
                {
                    _specialities.AddRange(listaCargada);
                }
            }
            else
            {
                Console.WriteLine($"[Persistencia] Advertencia: No se encontró el archivo en la ruta: {rutaArchivo}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Persistencia] Error crítico al cargar especialidades: {ex.Message}");
        }
    }



    public List<Speciality> GetSpecialities() => _specialities;

    public Speciality? GetSpecialityById(Guid id) => _specialities.Find(s => s.Id == id);


    public List<Doctor> GetDoctors() => _doctors;

    public Doctor? GetDoctorById(Guid id) => _doctors.Find(d => d.Id == id);

    public void AddDoctor(Doctor doctor)
    {
        if (doctor.Id == Guid.Empty)
        {
            doctor.Id = Guid.NewGuid();
        }
        _doctors.Add(doctor);
    }

    public void UpdateDoctor(Doctor doctor)
    {
        var existing = GetDoctorById(doctor.Id);
        if (existing != null)
        {
            existing.Name = doctor.Name;
            existing.LicenseNumber = doctor.LicenseNumber;
            existing.IsActive = doctor.IsActive;
            existing.Speciality = doctor.Speciality;
        }
    }
}