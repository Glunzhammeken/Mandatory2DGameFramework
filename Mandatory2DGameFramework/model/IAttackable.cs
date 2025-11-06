// I f.eks. model/IAttackable.cs

namespace Mandatory2DGameFramework.model
{
    /// <summary>
    /// Definerer en "rolle" for alt, der kan modtage skade.
    /// </summary>
    public interface IAttackable
    {
        /// <summary>
        /// Objektets nuværende livspoint.
        /// </summary>
        int HitPoint { get; set; }

        /// <summary>
        /// Objektets navn (godt til logging).
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Metoden, der kaldes, når objektet modtager skade.
        /// </summary>
        void ReceiveHit(int incomingHit);
    }
}