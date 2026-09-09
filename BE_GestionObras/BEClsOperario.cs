namespace BE_GestionObras
{
    public class BEClsOperario : BEClsPersona
    {
        // PROPIEDADES
        public int Legajo { get; set; }
        public string Especialidad { get; set; }

        // CONSTRUCTOR EXISTENTE - se mantiene.
        public BEClsOperario(string dni, string nombre, string apellido, string telefono,
                           int legajo, string especialidad)
            : base(dni, nombre, apellido, telefono)
        {
            Legajo = legajo;
            Especialidad = especialidad;
        }

        // CONSTRUCTOR SOBRECARGADO con IdCodigo y SueldoBase.
        public BEClsOperario(string idCodigo, string dni, string nombre, string apellido,
                            string telefono, double sueldoBase, int legajo, string especialidad)
            : base(idCodigo, dni, nombre, apellido, telefono, sueldoBase)
        {
            Legajo = legajo;
            Especialidad = especialidad;
        }

        // CONSTRUCTOR SIN PARAMETROS
        public BEClsOperario() : base()
        {
            Legajo = 0;
            Especialidad = string.Empty;
        }

        // POLIMORFISMO: el Operario calcula su sueldo con un 20% adicional.
        public override double CalcularSueldo()
        {
            return SueldoBase * 1.2;
        }
    }
}
