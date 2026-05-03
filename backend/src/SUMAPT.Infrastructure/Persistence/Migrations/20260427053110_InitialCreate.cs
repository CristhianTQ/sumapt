using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SUMAPT.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "mentoria");

            migrationBuilder.EnsureSchema(
                name: "comunicacion");

            migrationBuilder.EnsureSchema(
                name: "analitica");

            migrationBuilder.EnsureSchema(
                name: "academico");

            migrationBuilder.EnsureSchema(
                name: "auditoria");

            migrationBuilder.EnsureSchema(
                name: "auth");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:pgcrypto", ",,");

            migrationBuilder.CreateTable(
                name: "eventos_telemetria",
                schema: "analitica",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    SesionId = table.Column<Guid>(type: "uuid", nullable: true),
                    TipoEvento = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EntidadTipo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    EntidadId = table.Column<Guid>(type: "uuid", nullable: true),
                    Metadata = table.Column<string>(type: "jsonb", nullable: true),
                    RegistradoEn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventos_telemetria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "instituciones",
                schema: "academico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Nombre = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    NombreCorto = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LogoUrl = table.Column<string>(type: "text", nullable: true),
                    DominioEmail = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Pais = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ZonaHoraria = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Activa = table.Column<bool>(type: "boolean", nullable: false),
                    CreadoEn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_instituciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "log_acciones",
                schema: "auditoria",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: true),
                    RolActivo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Accion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EntidadTipo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EntidadId = table.Column<string>(type: "text", nullable: true),
                    DatosAntes = table.Column<string>(type: "jsonb", nullable: true),
                    DatosDespues = table.Column<string>(type: "jsonb", nullable: true),
                    IpOrigen = table.Column<string>(type: "text", nullable: true),
                    UserAgent = table.Column<string>(type: "text", nullable: true),
                    Resultado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    DetalleError = table.Column<string>(type: "text", nullable: true),
                    EjecutadoEn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_log_acciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    CreadoEn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    KeycloakId = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    AvatarUrl = table.Column<string>(type: "text", nullable: true),
                    ZonaHoraria = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Idioma = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    UltimoAccesoEn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreadoEn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ActualizadoEn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "metricas_agregadas",
                schema: "analitica",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    InstitucionId = table.Column<Guid>(type: "uuid", nullable: false),
                    PeriodoId = table.Column<Guid>(type: "uuid", nullable: true),
                    TipoMetrica = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Valor = table.Column<decimal>(type: "numeric(10,4)", nullable: false),
                    CalculadoEn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_metricas_agregadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_metricas_agregadas_instituciones_InstitucionId",
                        column: x => x.InstitucionId,
                        principalSchema: "academico",
                        principalTable: "instituciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "modelos_academicos",
                schema: "academico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    InstitucionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Tipo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PeriodosPorAño = table.Column<int>(type: "integer", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    CreadoEn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modelos_academicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_modelos_academicos_instituciones_InstitucionId",
                        column: x => x.InstitucionId,
                        principalSchema: "academico",
                        principalTable: "instituciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "periodos",
                schema: "academico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    InstitucionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FechaInicio = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaFin = table.Column<DateOnly>(type: "date", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    CreadoEn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_periodos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_periodos_instituciones_InstitucionId",
                        column: x => x.InstitucionId,
                        principalSchema: "academico",
                        principalTable: "instituciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "conversaciones",
                schema: "comunicacion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ParticipanteA = table.Column<Guid>(type: "uuid", nullable: false),
                    ParticipanteB = table.Column<Guid>(type: "uuid", nullable: false),
                    CreadoEn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conversaciones", x => x.Id);
                    table.CheckConstraint("CK_Conversacion_Distintos", "\"ParticipanteA\" <> \"ParticipanteB\"");
                    table.ForeignKey(
                        name: "FK_conversaciones_usuarios_ParticipanteA",
                        column: x => x.ParticipanteA,
                        principalSchema: "auth",
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_conversaciones_usuarios_ParticipanteB",
                        column: x => x.ParticipanteB,
                        principalSchema: "auth",
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "notificaciones",
                schema: "comunicacion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    DestinatarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    Tipo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Titulo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Cuerpo = table.Column<string>(type: "text", nullable: false),
                    Leida = table.Column<bool>(type: "boolean", nullable: false),
                    DatosExtra = table.Column<string>(type: "jsonb", nullable: true),
                    CreadoEn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LeidaEn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_notificaciones_usuarios_DestinatarioId",
                        column: x => x.DestinatarioId,
                        principalSchema: "auth",
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "perfiles_mentor",
                schema: "mentoria",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    Biografia = table.Column<string>(type: "text", nullable: true),
                    Especialidades = table.Column<string[]>(type: "text[]", nullable: false),
                    MaxEstudiantes = table.Column<short>(type: "smallint", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    CreadoEn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_perfiles_mentor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_perfiles_mentor_usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "auth",
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sesiones",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    KeycloakSid = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    IpOrigen = table.Column<string>(type: "text", nullable: true),
                    UserAgent = table.Column<string>(type: "text", nullable: true),
                    IniciadaEn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ExpiradaEn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Activa = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sesiones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sesiones_usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "auth",
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuario_roles",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    RolId = table.Column<Guid>(type: "uuid", nullable: false),
                    InstitucionId = table.Column<Guid>(type: "uuid", nullable: true),
                    AsignadoEn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    AsignadoPorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario_roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_usuario_roles_roles_RolId",
                        column: x => x.RolId,
                        principalSchema: "auth",
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_usuario_roles_usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "auth",
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "programas",
                schema: "academico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    InstitucionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModeloId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DuracionPeriodos = table.Column<int>(type: "integer", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    CreadoEn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_programas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_programas_instituciones_InstitucionId",
                        column: x => x.InstitucionId,
                        principalSchema: "academico",
                        principalTable: "instituciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_programas_modelos_academicos_ModeloId",
                        column: x => x.ModeloId,
                        principalSchema: "academico",
                        principalTable: "modelos_academicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "mensajes",
                schema: "comunicacion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ConversacionId = table.Column<Guid>(type: "uuid", nullable: false),
                    RemitenteId = table.Column<Guid>(type: "uuid", nullable: false),
                    Contenido = table.Column<string>(type: "text", nullable: false),
                    Leido = table.Column<bool>(type: "boolean", nullable: false),
                    EnviadoEn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LeidoEn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mensajes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mensajes_conversaciones_ConversacionId",
                        column: x => x.ConversacionId,
                        principalSchema: "comunicacion",
                        principalTable: "conversaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mensajes_usuarios_RemitenteId",
                        column: x => x.RemitenteId,
                        principalSchema: "auth",
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "disponibilidad",
                schema: "mentoria",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    MentorId = table.Column<Guid>(type: "uuid", nullable: false),
                    DiaSemana = table.Column<short>(type: "smallint", nullable: false),
                    HoraInicio = table.Column<TimeSpan>(type: "time", nullable: false),
                    HoraFin = table.Column<TimeSpan>(type: "time", nullable: false),
                    Modalidad = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Activa = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disponibilidad", x => x.Id);
                    table.CheckConstraint("CK_Disponibilidad_Horas", "\"HoraFin\" > \"HoraInicio\"");
                    table.ForeignKey(
                        name: "FK_disponibilidad_perfiles_mentor_MentorId",
                        column: x => x.MentorId,
                        principalSchema: "mentoria",
                        principalTable: "perfiles_mentor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "inscripciones",
                schema: "academico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    EstudianteId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProgramaId = table.Column<Guid>(type: "uuid", nullable: false),
                    PeriodoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Estado = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    CreadoEn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inscripciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_inscripciones_periodos_PeriodoId",
                        column: x => x.PeriodoId,
                        principalSchema: "academico",
                        principalTable: "periodos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_inscripciones_programas_ProgramaId",
                        column: x => x.ProgramaId,
                        principalSchema: "academico",
                        principalTable: "programas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_inscripciones_usuarios_EstudianteId",
                        column: x => x.EstudianteId,
                        principalSchema: "auth",
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "materias",
                schema: "academico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ProgramaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Creditos = table.Column<short>(type: "smallint", nullable: false),
                    PeriodoSugerido = table.Column<short>(type: "smallint", nullable: true),
                    Activa = table.Column<bool>(type: "boolean", nullable: false),
                    CreadoEn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_materias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_materias_programas_ProgramaId",
                        column: x => x.ProgramaId,
                        principalSchema: "academico",
                        principalTable: "programas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "citas",
                schema: "mentoria",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    MentorId = table.Column<Guid>(type: "uuid", nullable: false),
                    EstudianteId = table.Column<Guid>(type: "uuid", nullable: false),
                    InscripcionId = table.Column<Guid>(type: "uuid", nullable: true),
                    FechaHoraIni = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    FechaHoraFin = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Modalidad = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    EnlaceVirtual = table.Column<string>(type: "text", nullable: true),
                    Estado = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    CanceladoPor = table.Column<Guid>(type: "uuid", nullable: true),
                    MotivoCancel = table.Column<string>(type: "text", nullable: true),
                    CreadoEn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_citas", x => x.Id);
                    table.CheckConstraint("CK_Cita_Fechas", "\"FechaHoraFin\" > \"FechaHoraIni\"");
                    table.ForeignKey(
                        name: "FK_citas_inscripciones_InscripcionId",
                        column: x => x.InscripcionId,
                        principalSchema: "academico",
                        principalTable: "inscripciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_citas_perfiles_mentor_MentorId",
                        column: x => x.MentorId,
                        principalSchema: "mentoria",
                        principalTable: "perfiles_mentor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_citas_usuarios_EstudianteId",
                        column: x => x.EstudianteId,
                        principalSchema: "auth",
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "predicciones_riesgo",
                schema: "analitica",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    InscripcionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ScoreRiesgo = table.Column<decimal>(type: "numeric(5,4)", nullable: false),
                    NivelRiesgo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Factores = table.Column<string>(type: "jsonb", nullable: false),
                    VersionModelo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    GeneradoEn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    VigenteHasta = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_predicciones_riesgo", x => x.Id);
                    table.CheckConstraint("CK_Score_Rango", "\"ScoreRiesgo\" >= 0 AND \"ScoreRiesgo\" <= 1");
                    table.ForeignKey(
                        name: "FK_predicciones_riesgo_inscripciones_InscripcionId",
                        column: x => x.InscripcionId,
                        principalSchema: "academico",
                        principalTable: "inscripciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "historial_notas",
                schema: "academico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    InscripcionId = table.Column<Guid>(type: "uuid", nullable: false),
                    MateriaId = table.Column<Guid>(type: "uuid", nullable: false),
                    PeriodoId = table.Column<Guid>(type: "uuid", nullable: false),
                    NotaFinal = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    EstadoCurso = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Intentos = table.Column<short>(type: "smallint", nullable: false),
                    RegistradoEn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    RegistradoPor = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_historial_notas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_historial_notas_inscripciones_InscripcionId",
                        column: x => x.InscripcionId,
                        principalSchema: "academico",
                        principalTable: "inscripciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_historial_notas_materias_MateriaId",
                        column: x => x.MateriaId,
                        principalSchema: "academico",
                        principalTable: "materias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_historial_notas_periodos_PeriodoId",
                        column: x => x.PeriodoId,
                        principalSchema: "academico",
                        principalTable: "periodos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_historial_notas_usuarios_RegistradoPor",
                        column: x => x.RegistradoPor,
                        principalSchema: "auth",
                        principalTable: "usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "actas_sesion",
                schema: "mentoria",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    CitaId = table.Column<Guid>(type: "uuid", nullable: false),
                    TemasTratados = table.Column<string>(type: "text", nullable: false),
                    Compromisos = table.Column<string>(type: "text", nullable: true),
                    Observaciones = table.Column<string>(type: "text", nullable: true),
                    NivelRiesgoPercibido = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    CreadoEn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_actas_sesion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_actas_sesion_citas_CitaId",
                        column: x => x.CitaId,
                        principalSchema: "mentoria",
                        principalTable: "citas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_actas_sesion_CitaId",
                schema: "mentoria",
                table: "actas_sesion",
                column: "CitaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_citas_EstudianteId",
                schema: "mentoria",
                table: "citas",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_citas_InscripcionId",
                schema: "mentoria",
                table: "citas",
                column: "InscripcionId");

            migrationBuilder.CreateIndex(
                name: "IX_citas_MentorId",
                schema: "mentoria",
                table: "citas",
                column: "MentorId");

            migrationBuilder.CreateIndex(
                name: "IX_conversaciones_ParticipanteA_ParticipanteB",
                schema: "comunicacion",
                table: "conversaciones",
                columns: new[] { "ParticipanteA", "ParticipanteB" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_conversaciones_ParticipanteB",
                schema: "comunicacion",
                table: "conversaciones",
                column: "ParticipanteB");

            migrationBuilder.CreateIndex(
                name: "IX_disponibilidad_MentorId",
                schema: "mentoria",
                table: "disponibilidad",
                column: "MentorId");

            migrationBuilder.CreateIndex(
                name: "IX_eventos_telemetria_UsuarioId_RegistradoEn",
                schema: "analitica",
                table: "eventos_telemetria",
                columns: new[] { "UsuarioId", "RegistradoEn" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "IX_historial_notas_InscripcionId",
                schema: "academico",
                table: "historial_notas",
                column: "InscripcionId");

            migrationBuilder.CreateIndex(
                name: "IX_historial_notas_MateriaId",
                schema: "academico",
                table: "historial_notas",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_historial_notas_PeriodoId",
                schema: "academico",
                table: "historial_notas",
                column: "PeriodoId");

            migrationBuilder.CreateIndex(
                name: "IX_historial_notas_RegistradoPor",
                schema: "academico",
                table: "historial_notas",
                column: "RegistradoPor");

            migrationBuilder.CreateIndex(
                name: "IX_inscripciones_EstudianteId_ProgramaId_PeriodoId",
                schema: "academico",
                table: "inscripciones",
                columns: new[] { "EstudianteId", "ProgramaId", "PeriodoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_inscripciones_PeriodoId",
                schema: "academico",
                table: "inscripciones",
                column: "PeriodoId");

            migrationBuilder.CreateIndex(
                name: "IX_inscripciones_ProgramaId",
                schema: "academico",
                table: "inscripciones",
                column: "ProgramaId");

            migrationBuilder.CreateIndex(
                name: "IX_instituciones_NombreCorto",
                schema: "academico",
                table: "instituciones",
                column: "NombreCorto",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_log_acciones_EntidadTipo_EntidadId",
                schema: "auditoria",
                table: "log_acciones",
                columns: new[] { "EntidadTipo", "EntidadId" });

            migrationBuilder.CreateIndex(
                name: "IX_log_acciones_UsuarioId_EjecutadoEn",
                schema: "auditoria",
                table: "log_acciones",
                columns: new[] { "UsuarioId", "EjecutadoEn" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "IX_materias_ProgramaId_Codigo",
                schema: "academico",
                table: "materias",
                columns: new[] { "ProgramaId", "Codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_mensajes_ConversacionId_EnviadoEn",
                schema: "comunicacion",
                table: "mensajes",
                columns: new[] { "ConversacionId", "EnviadoEn" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "IX_mensajes_RemitenteId",
                schema: "comunicacion",
                table: "mensajes",
                column: "RemitenteId");

            migrationBuilder.CreateIndex(
                name: "IX_metricas_agregadas_InstitucionId",
                schema: "analitica",
                table: "metricas_agregadas",
                column: "InstitucionId");

            migrationBuilder.CreateIndex(
                name: "IX_modelos_academicos_InstitucionId",
                schema: "academico",
                table: "modelos_academicos",
                column: "InstitucionId");

            migrationBuilder.CreateIndex(
                name: "IX_notificaciones_DestinatarioId",
                schema: "comunicacion",
                table: "notificaciones",
                column: "DestinatarioId",
                filter: "\"Leida\" = FALSE");

            migrationBuilder.CreateIndex(
                name: "IX_perfiles_mentor_UsuarioId",
                schema: "mentoria",
                table: "perfiles_mentor",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_periodos_InstitucionId",
                schema: "academico",
                table: "periodos",
                column: "InstitucionId");

            migrationBuilder.CreateIndex(
                name: "IX_predicciones_riesgo_InscripcionId_GeneradoEn",
                schema: "analitica",
                table: "predicciones_riesgo",
                columns: new[] { "InscripcionId", "GeneradoEn" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "IX_programas_InstitucionId",
                schema: "academico",
                table: "programas",
                column: "InstitucionId");

            migrationBuilder.CreateIndex(
                name: "IX_programas_ModeloId",
                schema: "academico",
                table: "programas",
                column: "ModeloId");

            migrationBuilder.CreateIndex(
                name: "IX_roles_Nombre",
                schema: "auth",
                table: "roles",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sesiones_KeycloakSid",
                schema: "auth",
                table: "sesiones",
                column: "KeycloakSid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sesiones_UsuarioId",
                schema: "auth",
                table: "sesiones",
                column: "UsuarioId",
                filter: "\"Activa\" = TRUE");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_roles_RolId",
                schema: "auth",
                table: "usuario_roles",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_roles_UsuarioId_RolId_InstitucionId",
                schema: "auth",
                table: "usuario_roles",
                columns: new[] { "UsuarioId", "RolId", "InstitucionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_Email",
                schema: "auth",
                table: "usuarios",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_KeycloakId",
                schema: "auth",
                table: "usuarios",
                column: "KeycloakId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "actas_sesion",
                schema: "mentoria");

            migrationBuilder.DropTable(
                name: "disponibilidad",
                schema: "mentoria");

            migrationBuilder.DropTable(
                name: "eventos_telemetria",
                schema: "analitica");

            migrationBuilder.DropTable(
                name: "historial_notas",
                schema: "academico");

            migrationBuilder.DropTable(
                name: "log_acciones",
                schema: "auditoria");

            migrationBuilder.DropTable(
                name: "mensajes",
                schema: "comunicacion");

            migrationBuilder.DropTable(
                name: "metricas_agregadas",
                schema: "analitica");

            migrationBuilder.DropTable(
                name: "notificaciones",
                schema: "comunicacion");

            migrationBuilder.DropTable(
                name: "predicciones_riesgo",
                schema: "analitica");

            migrationBuilder.DropTable(
                name: "sesiones",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "usuario_roles",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "citas",
                schema: "mentoria");

            migrationBuilder.DropTable(
                name: "materias",
                schema: "academico");

            migrationBuilder.DropTable(
                name: "conversaciones",
                schema: "comunicacion");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "inscripciones",
                schema: "academico");

            migrationBuilder.DropTable(
                name: "perfiles_mentor",
                schema: "mentoria");

            migrationBuilder.DropTable(
                name: "periodos",
                schema: "academico");

            migrationBuilder.DropTable(
                name: "programas",
                schema: "academico");

            migrationBuilder.DropTable(
                name: "usuarios",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "modelos_academicos",
                schema: "academico");

            migrationBuilder.DropTable(
                name: "instituciones",
                schema: "academico");
        }
    }
}
