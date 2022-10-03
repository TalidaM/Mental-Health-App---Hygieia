using MentalHealthApp.Common;
using MentalHealthApp.DataAccess.Context;
using MentalHealthApp.Entities;
using MentalHealthApp.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentalHealthApp.DataAccess
{
    public class UnitOfWork
    {
        private readonly MentalHealthAppContext Context;

        public UnitOfWork(MentalHealthAppContext context)
        {
            this.Context = context;
        }
        private IRepository<IdentityUser> identityUsers;
        public IRepository<IdentityUser> IdentityUsers => identityUsers ?? (identityUsers = new BaseRepository<IdentityUser>(Context));

        private IRepository<IdentityRole> identityRoles;
        public IRepository<IdentityRole> IdentityRoles => identityRoles ?? (identityRoles = new BaseRepository<IdentityRole>(Context));


        private IRepository<Specialist> specialisti;
        public IRepository<Specialist> Specialisti => specialisti ?? (specialisti = new BaseRepository<Specialist>(Context));

        private IRepository<Utilizator> utilizatori;
        public IRepository<Utilizator> Utilizatori => utilizatori ?? (utilizatori = new BaseRepository<Utilizator>(Context));

        private IRepository<Programare> programari;
        public IRepository<Programare> Programari => programari ?? (programari = new BaseRepository<Programare>(Context));

        private IRepository<IstoricDiagnosticUtilizator> istoricDiagnosticUtilizatori;
        public IRepository<IstoricDiagnosticUtilizator> IstoricDiagnosticUtilizator => istoricDiagnosticUtilizatori ?? (istoricDiagnosticUtilizatori = new BaseRepository<IstoricDiagnosticUtilizator>(Context));

        private IRepository<Discutie> discutii;
        public IRepository<Discutie> Discutii => discutii ?? (discutii = new BaseRepository<Discutie>(Context));

        private IRepository<ComentariiDiscutie> comentariiDiscutii;
        public IRepository<ComentariiDiscutie> ComentariiDiscutii => comentariiDiscutii ?? (comentariiDiscutii = new BaseRepository<ComentariiDiscutie>(Context));

        private IRepository<CameraConferintum> cameraConferinte;
        public IRepository<CameraConferintum> CameraConferinte => cameraConferinte ?? (cameraConferinte = new BaseRepository<CameraConferintum>(Context));

        private IRepository<Adresa> adrese;
        public IRepository<Adresa> Adrese => adrese ?? (adrese = new BaseRepository<Adresa>(Context));

        private IRepository<Diagnostic> diagnostics;
        public IRepository<Diagnostic> Diagnostics => diagnostics ?? (diagnostics = new BaseRepository<Diagnostic>(Context));

        private IRepository<Simptome> simptom;
        public IRepository<Simptome> Simptom => simptom ?? (simptom = new BaseRepository<Simptome>(Context));

        private IRepository<Valabilitate> valabilitati;
        public IRepository<Valabilitate> Valabilitati => valabilitati ?? (valabilitati = new BaseRepository<Valabilitate>(Context));
        
        private IRepository<WorkDay> workDays;
        public IRepository<WorkDay> WorkDays => workDays ?? (workDays = new BaseRepository<WorkDay>(Context));

        private IRepository<ConditionsPost> conditionPosts;
        public IRepository<ConditionsPost> ConditionsPosts => conditionPosts ?? (conditionPosts = new BaseRepository<ConditionsPost>(Context));

        private IRepository<TopReads> topReads;
        public IRepository<TopReads> TopReads => topReads ?? (topReads = new BaseRepository<TopReads>(Context));

        private IRepository<MedicalReport> medicalReports;
        public IRepository<MedicalReport> MedicalReports => medicalReports ?? (medicalReports = new BaseRepository<MedicalReport>(Context));

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
