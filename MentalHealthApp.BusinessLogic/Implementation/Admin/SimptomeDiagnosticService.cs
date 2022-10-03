using MentalHealthApp.BusinessLogic.Base;
using MentalHealthApp.BusinessLogic.Implementation.Admin.ViewModels;
using MentalHealthApp.Common.DTOs;
using MentalHealthApp.Common.Exceptions;
using MentalHealthApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentalHealthApp.BusinessLogic.Implementation.Admin
{
    public class SimptomeDiagnosticService :BaseService
    {
        public SimptomeDiagnosticService(ServiceDependencies dependencies) : base(dependencies) { }

        public void AddSimptom(SDModel model)
        {
             ExecuteInTransaction(uow =>
            {
                var simptom = uow.Simptom.Get()
                                            .SingleOrDefault(s => s.Denumire.Equals(model.Denumire));
                if (simptom != null)
                {
                    string message = "Simptomul " + model.Denumire + " este deja inregistrat in baza de date";
                    throw new AlreadyExistsInDB(nameof(Simptome), message);
                }
                else
                {
                    var newSimptom = new Simptome();
                    newSimptom.Id = Guid.NewGuid();
                    newSimptom.Denumire = model.Denumire;
                    newSimptom.Descriere = model.Descriere;
                    uow.Simptom.Insert(newSimptom);
                    uow.SaveChanges();
                }

            });
           

        }

        public async Task<DSGenericDto> GetSimptomByDenumire(string name)
        {
            return ExecuteInTransaction(uow =>
            {
                var simptom = uow.Simptom.Get().Select(s => new DSGenericDto
                {
                    Denumire = s.Denumire,
                    Descriere = s.Descriere
                }).FirstOrDefault(s => s.Denumire.Equals(name));

                return simptom;

            });
           
        }

        public async Task<Simptome> ChangeSimptomDescription(Simptome simptome, string description)
        {
            return ExecuteInTransaction(uow =>
            {
                simptome.Descriere = description;
                uow.Simptom.Update(simptome);
                uow.SaveChanges();
                return simptome;
            });
            

        }

        
        //Diagnostic
        public void AddDiagnostic(SDModel model)
        {
            ExecuteInTransaction(uow =>
            {
                var diagnostic = uow.Diagnostics.Get()
                                                   .SingleOrDefault(d => d.Denumire.Equals(model.Denumire));
                if (diagnostic != null)
                {
                    string message = "Diagnosticul " + model.Denumire + " este deja inregistrat in baza de date";
                    throw new AlreadyExistsInDB(nameof(Diagnostic), message);
                }
                else
                {
                    var newdiagnostic = new Diagnostic();
                    newdiagnostic.Id = Guid.NewGuid();
                    newdiagnostic.Denumire = model.Denumire;
                    newdiagnostic.Descriere = model.Descriere;
                    uow.Diagnostics.Insert(newdiagnostic);
                    uow.SaveChanges();
                }
            });
            
        }
        public async Task<DSGenericDto> GetDiagnosticByDenumire(string name)
        {
            return ExecuteInTransaction(uow =>
            {
                var simptom = uow.Diagnostics.Get().Select(s => new DSGenericDto
                {
                    Denumire = s.Denumire,
                    Descriere = s.Descriere
                }).FirstOrDefault();

                return simptom;
            });
          
        }

        public async Task<Diagnostic> DeleteDiagnostic(string denumire)
        {
            return ExecuteInTransaction(uow =>
            {
                var diagnostic = uow.Diagnostics.Get().SingleOrDefault(d => d.Denumire.Equals(denumire));
                uow.Diagnostics.Delete(diagnostic);
                return diagnostic;
            });
            
        }

        public async Task<Diagnostic> ChangeDiagnosticFields(Diagnostic diagnostic, DSGenericDto fields)
        {
            return ExecuteInTransaction(uow =>
            {
                diagnostic.Denumire = fields.Denumire;
                diagnostic.Descriere = fields.Descriere;
                uow.Diagnostics.Update(diagnostic);
                uow.SaveChanges();
                return diagnostic;
            });
            

        }

    }
}
