namespace BE_GestionObras
{
    public class BEClsSupervisor : BEClsPersona
    {
        // PROPIEDADES
        public int IdSupervisor { get; set; }
        public string Sector { get; set; }

        // CONSTRUCTOR EXISTENTE - se mantiene.
        public BEClsSupervisor(string dni, string nombre, string apellido, string telefono,
                             int idSupervisor, string sector)
            : base(dni, nombre, apellido, telefono)
        {
            IdSupervisor = idSupervisor;
            Sector = sector;
        }

        // CONSTRUCTOR SOBRECARGADO con IdCodigo y SueldoBase.
        public BEClsSupervisor(string idCodigo, string dni, string nombre, string apellido,
                              string telefono, double sueldoBase, int idSupervisor, string sector)
            : base(idCodigo, dni, nombre, apellido, telefono, sueldoBase)
        {
            IdSupervisor = idSupervisor;
            Sector = sector;
        }

        // CONSTRUCTOR SIN PARAMETROS
        public BEClsSupervisor() : base()
        {
            IdSupervisor = 0;
            Sector = string.Empty;
        }

        // POLIMORFISMO: el Supervisor calcula su sueldo con un 50% adicional.
        public override double CalcularSueldo()
        {
            return SueldoBase * 1.5;
        }
    }
}
