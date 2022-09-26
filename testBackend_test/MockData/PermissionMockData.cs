using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace testBackend_test.MockData
{
    public class PermissionMockData
    {        
        public static IQueryable<Permission> GetPermissions()
        {

            var value = new List<Permission>
            {
                new Permission
                {
                    Id = 1,
                    NombreEmpleado = "Kevin",
                    ApellidoEmpleado = "Pincay",
                    TipoPermisoId = 1,
                    FechaPermiso = DateTimeOffset.Now,
                },
                new Permission
                {
                    Id = 2,
                    NombreEmpleado = "Boris",
                    ApellidoEmpleado = "Pincay",
                    TipoPermisoId = 2,
                    FechaPermiso = DateTimeOffset.Now,
                },
                new Permission
                {
                    Id = 3,
                    NombreEmpleado = "Alexa",
                    ApellidoEmpleado = "Pincay",
                    TipoPermisoId = 2,
                    FechaPermiso = DateTimeOffset.Now,
                }
            };
            return value.AsQueryable();
        }
        public static IQueryable<Permission> GetEmptyPermissions()
        {            
            return Enumerable.Empty<Permission>().AsQueryable();
        }

        public static Permission NewPermission()
        {
            return new Permission
            {
                Id = 0,
                NombreEmpleado = "Usuario",
                ApellidoEmpleado = "Deprueba",
                TipoPermisoId = 2,
                FechaPermiso = DateTime.Now,
            };
        }
    }
}
