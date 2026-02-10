using Data.Identity;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data;

/// <summary>
/// Контекст данных для работы с базой данных реабилитационной системы
/// </summary>
public class DataContext : DbContext
{
    // ====================
    // USERS & PATIENTS
    // ====================
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Patient> Patients { get; set; } = null!;
    public DbSet<PatientAssignment> PatientAssignments { get; set; } = null!;
    public DbSet<VisitNote> VisitNotes { get; set; } = null!;

    // ====================
    // CONDITIONS & STAGES
    // ====================
    public DbSet<Condition> Conditions { get; set; } = null!;
    public DbSet<PatientCondition> PatientConditions { get; set; } = null!;
    public DbSet<RehabProtocol> RehabProtocols { get; set; } = null!;
    public DbSet<RehabStage> RehabStages { get; set; } = null!;
    public DbSet<StageCriterion> StageCriteria { get; set; } = null!;
    public DbSet<PatientStageStatus> PatientStageStatuses { get; set; } = null!;

    // ====================
    // EXERCISES & MEDIA
    // ====================
    public DbSet<Exercise> Exercises { get; set; } = null!;
    public DbSet<MediaAsset> MediaAssets { get; set; } = null!;
    public DbSet<ExerciseMedia> ExerciseMedia { get; set; } = null!;

    // ====================
    // TEMPLATES
    // ====================
    public DbSet<Template> Templates { get; set; } = null!;
    public DbSet<TemplateItem> TemplateItems { get; set; } = null!;

    // ====================
    // WORKOUTS
    // ====================
    public DbSet<WorkoutPlan> WorkoutPlans { get; set; } = null!;
    public DbSet<WorkoutSession> WorkoutSessions { get; set; } = null!;
    public DbSet<WorkoutExercise> WorkoutExercises { get; set; } = null!;

    // ====================
    // LOGS & PROGRESS
    // ====================
    public DbSet<ExerciseLog> ExerciseLogs { get; set; } = null!;
    public DbSet<ExerciseLogMedia> ExerciseLogMedia { get; set; } = null!;
    public DbSet<ProgressEntry> ProgressEntries { get; set; } = null!;

    // ====================
    // TESTS
    // ====================
    public DbSet<TestDefinition> TestDefinitions { get; set; } = null!;
    public DbSet<ConditionTestRecommendation> ConditionTestRecommendations { get; set; } = null!;
    public DbSet<TestResult> TestResults { get; set; } = null!;

    // ====================
    // CONSTRUCTOR
    // ====================
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    // ====================
    // MODEL CONFIGURATION
    // ====================
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Настройка конвертеров для JSON-полей
        ConfigureJsonConverters(modelBuilder);

        // Настройка отношений (опционально, так как атрибуты уже есть)
        //ConfigureRelationships(modelBuilder);

        // Глобальные фильтры (опционально)
        //ConfigureGlobalFilters(modelBuilder);

        // Настройка соглашений
        ConfigureConventions(modelBuilder);
    }

    // ====================
    // JSON CONVERTERS
    // ====================
    private void ConfigureJsonConverters(ModelBuilder modelBuilder)
    {
        // Универсальный конвертер для JSON-полей
        var jsonConverter = new ValueConverter<string, string>(
            v => v,
            v => v
        );

        // Список всех сущностей с JSON-полями
        var jsonProperties = new[]
        {
            (typeof(Patient), nameof(Patient.TagsJson)),
            (typeof(VisitNote), nameof(VisitNote.ExtraJson)),
            (typeof(Exercise), nameof(Exercise.ParamsSchemaJson)),
            (typeof(TemplateItem), nameof(TemplateItem.ParamsJson)),
            (typeof(WorkoutExercise), nameof(WorkoutExercise.ParamsJson)),
            (typeof(ExerciseLog), nameof(ExerciseLog.MetricsJson)),
            (typeof(ProgressEntry), nameof(ProgressEntry.PainJson)),
            (typeof(ProgressEntry), nameof(ProgressEntry.RomJson)),
            (typeof(TestDefinition), nameof(TestDefinition.ResultSchemaJson)),
            (typeof(TestResult), nameof(TestResult.ResultJson)),
            (typeof(StageCriterion), nameof(StageCriterion.CriteriaJson))
        };

        foreach (var (entityType, propertyName) in jsonProperties)
        {
            modelBuilder.Entity(entityType)
                .Property(propertyName)
                .HasConversion(jsonConverter)
                .HasColumnType("jsonb") // Для PostgreSQL, используйте "json" для SQL Server
                .IsRequired(false);
        }
    }

    // ====================
    // RELATIONSHIPS
    // ====================
    //private void ConfigureRelationships(ModelBuilder modelBuilder)
    //{
    //    // User -> Patient (один ко многим)
    //    modelBuilder.Entity<User>()
    //        .HasMany(u => u.ManagedPatients)
    //        .WithOne(p => p.User)
    //        .HasForeignKey(p => p.UserId)
    //        .OnDelete(DeleteBehavior.Restrict);

    //    // Patient -> PatientAssignment (один ко многим)
    //    modelBuilder.Entity<Patient>()
    //        .HasMany(p => p.Assignments)
    //        .WithOne(a => a.Patient)
    //        .HasForeignKey(a => a.PatientId)
    //        .OnDelete(DeleteBehavior.Cascade);

    //    // User -> PatientAssignment (один ко многим)
    //    modelBuilder.Entity<User>()
    //        .HasMany(u => u.AssignedPatients)
    //        .WithOne(a => a.Therapist)
    //        .HasForeignKey(a => a.UserId)
    //        .OnDelete(DeleteBehavior.Restrict);

    //    // Patient -> VisitNote (один ко многим)
    //    modelBuilder.Entity<Patient>()
    //        .HasMany(p => p.VisitNotes)
    //        .WithOne(n => n.Patient)
    //        .HasForeignKey(n => n.PatientId)
    //        .OnDelete(DeleteBehavior.Cascade);

    //    // User -> VisitNote (один ко многим)
    //    modelBuilder.Entity<User>()
    //        .HasMany(u => u.AuthoredVisitNotes)
    //        .WithOne(n => n.Author)
    //        .HasForeignKey(n => n.AuthorUserId)
    //        .OnDelete(DeleteBehavior.Restrict);

    //    // Patient -> PatientCondition (один ко многим)
    //    modelBuilder.Entity<Patient>()
    //        .HasMany(p => p.Conditions)
    //        .WithOne(pc => pc.Patient)
    //        .HasForeignKey(pc => pc.PatientId)
    //        .OnDelete(DeleteBehavior.Cascade);

    //    // Condition -> PatientCondition (один ко многим)
    //    modelBuilder.Entity<Condition>()
    //        .HasMany(c => c.PatientConditions)
    //        .WithOne(pc => pc.Condition)
    //        .HasForeignKey(pc => pc.ConditionId)
    //        .OnDelete(DeleteBehavior.Restrict);

    //    // User -> PatientCondition (один ко многим)
    //    modelBuilder.Entity<User>()
    //        .HasMany(u => u.PatientConditions)
    //        .WithOne(pc => pc.CreatedBy)
    //        .HasForeignKey(pc => pc.CreatedByUserId)
    //        .OnDelete(DeleteBehavior.Restrict);

    //    // PatientCondition -> PatientStageStatus (один ко многим)
    //    modelBuilder.Entity<PatientCondition>()
    //        .HasMany(pc => pc.StageStatuses)
    //        .WithOne(pss => pss.PatientCondition)
    //        .HasForeignKey(pss => pss.PatientConditionId)
    //        .OnDelete(DeleteBehavior.Cascade);

    //    // RehabProtocol -> RehabStage (один ко многим)
    //    modelBuilder.Entity<RehabProtocol>()
    //        .HasMany(rp => rp.Stages)
    //        .WithOne(rs => rs.Protocol)
    //        .HasForeignKey(rs => rs.ProtocolId)
    //        .OnDelete(DeleteBehavior.Cascade);

    //    // RehabStage -> StageCriterion (один ко многим)
    //    modelBuilder.Entity<RehabStage>()
    //        .HasMany(rs => rs.Criteria)
    //        .WithOne(sc => sc.Stage)
    //        .HasForeignKey(sc => sc.StageId)
    //        .OnDelete(DeleteBehavior.Cascade);

    //    // Patient -> WorkoutPlan (один ко многим)
    //    modelBuilder.Entity<Patient>()
    //        .HasMany(p => p.WorkoutPlans)
    //        .WithOne(wp => wp.Patient)
    //        .HasForeignKey(wp => wp.PatientId)
    //        .OnDelete(DeleteBehavior.Cascade);

    //    // User -> WorkoutPlan (один ко многим)
    //    modelBuilder.Entity<User>()
    //        .HasMany(u => u.WorkoutPlans)
    //        .WithOne(wp => wp.CreatedBy)
    //        .HasForeignKey(wp => wp.CreatedByUserId)
    //        .OnDelete(DeleteBehavior.Restrict);

    //    // Template -> WorkoutPlan (один ко многим)
    //    modelBuilder.Entity<Template>()
    //        .HasMany(t => t.WorkoutPlans)
    //        .WithOne(wp => wp.SourceTemplate)
    //        .HasForeignKey(wp => wp.SourceTemplateId)
    //        .OnDelete(DeleteBehavior.Restrict);

    //    // WorkoutPlan -> WorkoutSession (один ко многим)
    //    modelBuilder.Entity<WorkoutPlan>()
    //        .HasMany(wp => wp.Sessions)
    //        .WithOne(ws => ws.WorkoutPlan)
    //        .HasForeignKey(ws => ws.WorkoutPlanId)
    //        .OnDelete(DeleteBehavior.Cascade);

    //    // Patient -> WorkoutSession (один ко многим)
    //    modelBuilder.Entity<Patient>()
    //        .HasMany(p => p.WorkoutSessions)
    //        .WithOne(ws => ws.Patient)
    //        .HasForeignKey(ws => ws.PatientId)
    //        .OnDelete(DeleteBehavior.Restrict);

    //    // WorkoutSession -> WorkoutExercise (один ко многим)
    //    modelBuilder.Entity<WorkoutSession>()
    //        .HasMany(ws => ws.Exercises)
    //        .WithOne(we => we.Session)
    //        .HasForeignKey(we => we.WorkoutSessionId)
    //        .OnDelete(DeleteBehavior.Cascade);

    //    // Exercise -> WorkoutExercise (один ко многим)
    //    modelBuilder.Entity<Exercise>()
    //        .HasMany(e => e.WorkoutExercises)
    //        .WithOne(we => we.Exercise)
    //        .HasForeignKey(we => we.ExerciseId)
    //        .OnDelete(DeleteBehavior.Restrict);

    //    // WorkoutExercise -> ExerciseLog (один ко многим)
    //    modelBuilder.Entity<WorkoutExercise>()
    //        .HasMany(we => we.Logs)
    //        .WithOne(el => el.WorkoutExercise)
    //        .HasForeignKey(el => el.WorkoutExerciseId)
    //        .OnDelete(DeleteBehavior.Cascade);

    //    // Patient -> ExerciseLog (один ко многим)
    //    modelBuilder.Entity<Patient>()
    //        .HasMany(p => p.ExerciseLogs)
    //        .WithOne(el => el.Patient)
    //        .HasForeignKey(el => el.PatientId)
    //        .OnDelete(DeleteBehavior.Restrict);

    //    // User -> ExerciseLog (один ко многим)
    //    modelBuilder.Entity<User>()
    //        .HasMany(u => u.ExerciseLog)
    //        .WithOne(el => el.CreatedBy)
    //        .HasForeignKey(el => el.CreatedByUserId)
    //        .OnDelete(DeleteBehavior.Restrict);

    //    // ExerciseLog -> ExerciseLogMedia (один ко многим)
    //    modelBuilder.Entity<ExerciseLog>()
    //        .HasMany(el => el.Media)
    //        .WithOne(elm => elm.ExerciseLog)
    //        .HasForeignKey(elm => elm.ExerciseLogId)
    //        .OnDelete(DeleteBehavior.Cascade);

    //    // MediaAsset -> ExerciseLogMedia (один ко многим)
    //    modelBuilder.Entity<MediaAsset>()
    //        .HasMany(ma => ma.ExerciseLogMedia)
    //        .WithOne(elm => elm.MediaAsset)
    //        .HasForeignKey(elm => elm.MediaAssetId)
    //        .OnDelete(DeleteBehavior.Restrict);

    //    // Patient -> ProgressEntry (один ко многим)
    //    modelBuilder.Entity<Patient>()
    //        .HasMany(p => p.ProgressEntries)
    //        .WithOne(pe => pe.Patient)
    //        .HasForeignKey(pe => pe.PatientId)
    //        .OnDelete(DeleteBehavior.Cascade);

    //    // WorkoutSession -> ProgressEntry (один ко многим)
    //    modelBuilder.Entity<WorkoutSession>()
    //        .HasMany(ws => ws.ProgressEntries)
    //        .WithOne(pe => pe.Session)
    //        .HasForeignKey(pe => pe.WorkoutSessionId)
    //        .OnDelete(DeleteBehavior.Restrict);

    //    // User -> ProgressEntry (один ко многим)
    //    modelBuilder.Entity<User>()
    //        .HasMany(u => u.ProgressEntries)
    //        .WithOne(pe => pe.CreatedBy)
    //        .HasForeignKey(pe => pe.CreatedByUserId)
    //        .OnDelete(DeleteBehavior.Restrict);

    //    // User -> TestDefinition (один ко многим)
    //    modelBuilder.Entity<User>()
    //        .HasMany(u => u.TestDefinitions)
    //        .WithOne(td => td.Owner)
    //        .HasForeignKey(td => td.OwnerId)
    //        .OnDelete(DeleteBehavior.Restrict);

    //    // Condition -> ConditionTestRecommendation (один ко многим)
    //    modelBuilder.Entity<Condition>()
    //        .HasMany(c => c.TestRecommendations)
    //        .WithOne(ctr => ctr.Condition)
    //        .HasForeignKey(ctr => ctr.ConditionId)
    //        .OnDelete(DeleteBehavior.Cascade);

    //    // TestDefinition -> ConditionTestRecommendation (один ко многим)
    //    modelBuilder.Entity<TestDefinition>()
    //        .HasMany(td => td.Recommendations)
    //        .WithOne(ctr => ctr.TestDefinition)
    //        .HasForeignKey(ctr => ctr.TestDefinitionId)
    //        .OnDelete(DeleteBehavior.Cascade);

    //    // Patient -> TestResult (один ко многим)
    //    modelBuilder.Entity<Patient>()
    //        .HasMany(p => p.TestResults)
    //        .WithOne(tr => tr.Patient)
    //        .HasForeignKey(tr => tr.PatientId)
    //        .OnDelete(DeleteBehavior.Cascade);

    //    // PatientCondition -> TestResult (один ко многим)
    //    modelBuilder.Entity<PatientCondition>()
    //        .HasMany(pc => pc.TestResults)
    //        .WithOne(tr => tr.PatientCondition)
    //        .HasForeignKey(tr => tr.PatientConditionId)
    //        .OnDelete(DeleteBehavior.Restrict);

    //    // TestDefinition -> TestResult (один ко многим)
    //    modelBuilder.Entity<TestDefinition>()
    //        .HasMany(td => td.Results)
    //        .WithOne(tr => tr.TestDefinition)
    //        .HasForeignKey(tr => tr.TestDefinitionId)
    //        .OnDelete(DeleteBehavior.Restrict);

    //    // WorkoutSession -> TestResult (один ко многим)
    //    modelBuilder.Entity<WorkoutSession>()
    //        .HasMany(ws => ws.TestResults)
    //        .WithOne(tr => tr.Session)
    //        .HasForeignKey(tr => tr.WorkoutSessionId)
    //        .OnDelete(DeleteBehavior.Restrict);

    //    // User -> TestResult (один ко многим)
    //    modelBuilder.Entity<User>()
    //        .HasMany(u => u.TestResults)
    //        .WithOne(tr => tr.CreatedBy)
    //        .HasForeignKey(tr => tr.CreatedByUserId)
    //        .OnDelete(DeleteBehavior.Restrict);

    //    // Condition -> RehabProtocol (один ко многим)
    //    modelBuilder.Entity<Condition>()
    //        .HasMany(c => c.RehabProtocols)
    //        .WithOne(rp => rp.Condition)
    //        .HasForeignKey(rp => rp.ConditionId)
    //        .OnDelete(DeleteBehavior.Cascade);

    //    // User -> RehabProtocol (один ко многим)
    //    modelBuilder.Entity<User>()
    //        .HasMany(u => u.RehabProtocols)
    //        .WithOne(rp => rp.Owner)
    //        .HasForeignKey(rp => rp.OwnerId)
    //        .OnDelete(DeleteBehavior.Restrict);

    //    // RehabProtocol -> PatientStageStatus (один ко многим)
    //    modelBuilder.Entity<RehabProtocol>()
    //        .HasMany(rp => rp.PatientStatuses)
    //        .WithOne(pss => pss.Protocol)
    //        .HasForeignKey(pss => pss.ProtocolId)
    //        .OnDelete(DeleteBehavior.Restrict);

    //    // RehabStage -> PatientStageStatus (один ко многим)
    //    modelBuilder.Entity<RehabStage>()
    //        .HasMany(rs => rs.PatientStatuses)
    //        .WithOne(pss => pss.CurrentStage)
    //        .HasForeignKey(pss => pss.CurrentStageId)
    //        .OnDelete(DeleteBehavior.Restrict);

    //    // User -> PatientStageStatus (один ко многим)
    //    modelBuilder.Entity<User>()
    //        .HasMany(u => u.PatientStageStatuses)
    //        .WithOne(pss => pss.ChangedBy)
    //        .HasForeignKey(pss => pss.ChangedByUserId)
    //        .OnDelete(DeleteBehavior.Restrict);

    //    // Exercise -> ExerciseMedia (один ко многим)
    //    modelBuilder.Entity<Exercise>()
    //        .HasMany(e => e.ExerciseMedia)
    //        .WithOne(em => em.Exercise)
    //        .HasForeignKey(em => em.ExerciseId)
    //        .OnDelete(DeleteBehavior.Cascade);

    //    // MediaAsset -> ExerciseMedia (один ко многим)
    //    modelBuilder.Entity<MediaAsset>()
    //        .HasMany(ma => ma.ExerciseMedia)
    //        .WithOne(em => em.MediaAsset)
    //        .HasForeignKey(em => em.MediaAssetId)
    //        .OnDelete(DeleteBehavior.Restrict);

    //    // Template -> TemplateItem (один ко многим)
    //    modelBuilder.Entity<Template>()
    //        .HasMany(t => t.Items)
    //        .WithOne(ti => ti.Template)
    //        .HasForeignKey(ti => ti.TemplateId)
    //        .OnDelete(DeleteBehavior.Cascade);

    //    // Exercise -> TemplateItem (один ко многим)
    //    modelBuilder.Entity<Exercise>()
    //        .HasMany(e => e.TemplateItems)
    //        .WithOne(ti => ti.Exercise)
    //        .HasForeignKey(ti => ti.ExerciseId)
    //        .OnDelete(DeleteBehavior.Restrict);

    //    // User -> MediaAsset (один ко многим)
    //    modelBuilder.Entity<User>()
    //        .HasMany(u => u.MediaAssets)
    //        .WithOne(ma => ma.UploadedBy)
    //        .HasForeignKey(ma => ma.UploadedByUserId)
    //        .OnDelete(DeleteBehavior.Restrict);

    //    // Patient -> MediaAsset (один ко многим)
    //    modelBuilder.Entity<Patient>()
    //        .HasMany(p => p.MediaAssets)
    //        .WithOne(ma => ma.Patient)
    //        .HasForeignKey(ma => ma.PatientId)
    //        .OnDelete(DeleteBehavior.Restrict);
    //}

    // ====================
    // GLOBAL FILTERS
    // ====================
    //private void ConfigureGlobalFilters(ModelBuilder modelBuilder)
    //{
    //    // Пример глобального фильтра для активных записей
    //    // Раскомментируйте, если добавите поле IsDeleted

    //    // modelBuilder.Entity<User>()
    //    //     .HasQueryFilter(u => u.Status != "deleted");
    //    //
    //    // modelBuilder.Entity<Patient>()
    //    //     .HasQueryFilter(p => p.User == null || p.User.Status != "deleted");
    //}

    // ====================
    // CONVENTIONS
    // ====================
    private void ConfigureConventions(ModelBuilder modelBuilder)
    {
        // Автоматическое преобразование всех строк в snake_case для имен колонок
        // (если не указан атрибут [Column])
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                // Пропускаем свойства, у которых уже есть атрибут [Column]
                if (property.GetColumnName(StoreObjectIdentifier.Create(entityType, StoreObjectType.Table)!.Value) == null)
                {
                    // Можно добавить кастомное именование здесь
                }
            }
        }
    }

    // ====================
    // SAVE CHANGES
    // ====================
    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
        var entries = ChangeTracker.Entries<BaseEntity>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            if (entry.Entity is User user && entry.State == EntityState.Added)
            {
                // created_at устанавливается базой данных
            }
            // Аналогично для других сущностей
        }
    }

    // ====================
    // HELPER METHODS
    // ====================
    /// <summary>
    /// Проверяет, существует ли таблица в базе данных
    /// </summary>
    public async Task<bool> TableExistsAsync(string tableName)
    {
        var sql = Database.ProviderName switch
        {
            "Npgsql.EntityFrameworkCore.PostgreSQL" =>
                $"SELECT EXISTS (SELECT FROM information_schema.tables WHERE table_schema = 'public' AND table_name = '{tableName}');",
            "Microsoft.EntityFrameworkCore.SqlServer" =>
                $"SELECT CASE WHEN EXISTS (SELECT 1 FROM sys.tables WHERE name = '{tableName}') THEN 1 ELSE 0 END;",
            _ => throw new NotSupportedException($"Provider {Database.ProviderName} not supported")
        };

        var result = await Database.SqlQueryRaw<int>(sql).FirstOrDefaultAsync();
        return result == 1;
    }

    /// <summary>
    /// Получает количество записей в таблице
    /// </summary>
    public async Task<int> GetTableCountAsync<TEntity>() where TEntity : BaseEntity
    {
        return await Set<TEntity>().CountAsync();
    }
}