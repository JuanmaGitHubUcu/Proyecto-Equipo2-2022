namespace NavalBattle
{
    /// <summary>
    /// Coords representa las coordenadas, que ocupan cada una una cuadrícula del tablero.
    /// </summary>
    public class Coords
    {
        /// <summary>
        /// String de la coordenada.
        /// </summary>
        private string coordsLocation;

        /// <summary>
        /// Estado de la coordenada.
        /// </summary>
        private bool hasBeenAttacked = false;

        /// <summary>
        /// Constructor de Coords.
        /// </summary>
        /// <param name="aCoordsLocation"></param>
        public Coords(string aCoordsLocation)
        {
            this.coordsLocation = aCoordsLocation;
        }

        /// <summary>
        /// Gets de CoordsLocation.
        /// </summary>
        /// <value></value>
        public string CoordsLocation
        {
            get
            {
                return this.coordsLocation;
            }
        }

        /// <summary>
        /// Gets de HasBeenAttacked.
        /// </summary>
        /// <value></value>
        public bool HasBeenAttacked
        {
            get
            {
                return this.hasBeenAttacked;
            }
        }

        /// <summary>
        /// Devuelve true si dos coordenadas son iguales.
        /// </summary>
        /// <param name="coord"></param>
        /// <returns></returns>
        public bool CoordsEquals(Coords coord)
        {
            return this.CoordsLocation == coord.coordsLocation;
        }

        /// <summary>
        /// Cambia el estado de una coordena que fue atacada.
        /// </summary>
        public void ChangeCoordState()
        {
            this.hasBeenAttacked = true;
        }
    }
}