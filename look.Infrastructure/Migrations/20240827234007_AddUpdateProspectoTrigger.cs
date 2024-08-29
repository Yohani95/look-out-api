using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace look.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdateProspectoTrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Crear Trigger para AFTER INSERT
            migrationBuilder.Sql(@"
            CREATE TRIGGER update_prospecto_after_call
            AFTER INSERT ON llamadaprospectos
            FOR EACH ROW
            BEGIN
                DECLARE total_calls INT;
                DECLARE total_responses INT;

                -- Cuenta el número total de llamadas para el IdProspecto
                SELECT COUNT(*) INTO total_calls
                FROM llamadaprospectos
                WHERE IdProspecto = NEW.IdProspecto;

                -- Cuenta el número total de respuestas positivas
                SELECT COUNT(*) INTO total_responses
                FROM llamadaprospectos
                WHERE IdProspecto = NEW.IdProspecto AND RespondeLlamada = 1;

                -- Actualiza la tabla prospecto
                UPDATE prospecto
                SET 
                    Contactado = IF(total_calls > 0, 1, 0),
                    CantidadLlamadas = total_calls,
                    Responde = IF(total_calls > 0 AND total_responses = 0, 0, 1)
                WHERE id = NEW.IdProspecto;
            END;
        ");

            // Crear Trigger para AFTER UPDATE
            migrationBuilder.Sql(@"
            CREATE TRIGGER update_prospecto_after_update
            AFTER UPDATE ON llamadaprospectos
            FOR EACH ROW
            BEGIN
                DECLARE total_calls INT;
                DECLARE total_responses INT;

                -- Cuenta el número total de llamadas para el IdProspecto
                SELECT COUNT(*) INTO total_calls
                FROM llamadaprospectos
                WHERE IdProspecto = NEW.IdProspecto;

                -- Cuenta el número total de respuestas positivas
                SELECT COUNT(*) INTO total_responses
                FROM llamadaprospectos
                WHERE IdProspecto = NEW.IdProspecto AND RespondeLlamada = 1;

                -- Actualiza la tabla prospecto
                UPDATE prospecto
                SET 
                    Contactado = IF(total_calls > 0, 1, 0),
                    CantidadLlamadas = total_calls,
                    Responde = IF(total_calls > 0 AND total_responses = 0, 0, 1)
                WHERE id = NEW.IdProspecto;
            END;
        ");

            // Crear Trigger para AFTER DELETE
            migrationBuilder.Sql(@"
            CREATE TRIGGER update_prospecto_after_delete
            AFTER DELETE ON llamadaprospectos
            FOR EACH ROW
            BEGIN
                DECLARE total_calls INT;
                DECLARE total_responses INT;

                -- Cuenta el número total de llamadas para el IdProspecto
                SELECT COUNT(*) INTO total_calls
                FROM llamadaprospectos
                WHERE IdProspecto = OLD.IdProspecto;

                -- Cuenta el número total de respuestas positivas
                SELECT COUNT(*) INTO total_responses
                FROM llamadaprospectos
                WHERE IdProspecto = OLD.IdProspecto AND RespondeLlamada = 1;

                -- Actualiza la tabla prospecto
                UPDATE prospecto
                SET 
                    Contactado = IF(total_calls > 0, 1, 0),
                    CantidadLlamadas = total_calls,
                    Responde = IF(total_calls > 0 AND total_responses = 0, 0, 1)
                WHERE id = OLD.IdProspecto;
            END;
        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar Triggers en caso de rollback
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS update_prospecto_after_call;");
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS update_prospecto_after_update;");
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS update_prospecto_after_delete;");
        }
    }
}
