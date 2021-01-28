using System;
using System.Threading;

namespace JeuSimon
{
    /// <summary>
    /// Auteur: Zachary Denis
    /// Description: Vous devez concevoir une application de type console basée sur le jeu
    ///              de Simon simplifié. Le jeu consiste à allumer une couleur, puis ajouter
    ///              au hasard une nouvelle couleur par la suite. Le joueur doit reproduire
    ///              cette nouvelle séquence. Chaque fois que le joueur reproduit correctement
    ///              la séquence, le jeu ajoute une nouvelle couleur supplémentaire.
    /// Date: 2020-11-09
    /// </summary>
    class Program
    {
        #region Variables globales
        static private Random _rnd = new Random();
        static private byte[] _abyCoup = new byte[255];
        static private byte _bySéquence = 0;
        static private byte _byRecord = 0;
        static private string _sRéponse = "";
        #endregion

        #region Méthodes
        /// <summary>
        /// Auteur: Zachary Denis
        /// Description: Prend un chiffre puis retourne la couleur lier.
        /// </summary>
        /// <param name="iCouleur"></param>
        /// <returns>Retourne une couleur selon le chiffre en entrée</returns>
        static private String ChercheCouleur(int iCouleur)
        {
            string sColor = "";
            switch (iCouleur)
            {
                case 1: sColor = "Green"; break;
                case 2: sColor = "Red"; break;
                case 3: sColor = "Yellow"; break;
                case 4: sColor = "Blue"; break;
                case 5: sColor = "White"; break;
                default: break;
            }
            return sColor;
        }
        /// <summary>
        /// Auteur: Zachary Denis
        /// Description: Affiche le jeu principale, si la couleur d'entrer est de 1,2,3,4 La case sera blanche 
        /// Date: 2020-11-17
        /// </summary>
        /// <param name="iCoup"></param>
        static private void AfficherJeu(int iCoup)
        {
            Console.Clear();
            Console.WriteLine("============= Jeu de Simon ============");
            for (int iLigne = 1; iLigne <= 3; iLigne++) // 3 ligne de couleur
            {
                if (iLigne != 2) // Check si nous somme a la 2ème ligne de couleur
                {
                    for (int iCpt = 1; iCpt <= 4; iCpt++) // Exécute les 4 cases de couleurs.
                    {
                        if (iCoup != iCpt)
                        {
                            for (int iCpt1 = 1; iCpt1 <= 9; iCpt1++)
                            {
                                Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), ChercheCouleur(iCpt)); // Match la couleur avec le cherche couleur selon le cpt
                                Console.Write(" ");
                            }
                            Console.ResetColor();
                            if (iCpt != 4) // Si on n'est pas a la derniere caes de couleur alors mettre "|"
                                Console.Write("|");
                        }
                        else
                        {
                            for (int iCpt1 = 1; iCpt1 <= 9; iCpt1++) 
                            {
                                Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), ChercheCouleur(5)); // Match avec la couleur blanche
                                Console.Write(" ");
                            }
                            Console.ResetColor();
                            if (iCpt != 4)
                                Console.Write("|");
                        }
                    }
                    Console.WriteLine();
                    Console.ResetColor();
                }
                else
                {
                    for (int iCpt = 1; iCpt <= 4; iCpt++)
                    {
                        if (iCoup != iCpt)
                        {
                            for (int iCpt1 = 1; iCpt1 <= 4; iCpt1++)
                            {
                                Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), ChercheCouleur(iCpt)); // On écrit 4 espace de couleur.
                                Console.Write(" ");
                            }
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(iCpt); // Écrit le chiffre
                            for (int iCpt1 = 1; iCpt1 <= 4; iCpt1++)
                            {
                                Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), ChercheCouleur(iCpt)); // On écrit 4 espace de couleur.
                                Console.Write(" ");
                            }
                            Console.ResetColor();
                            if (iCpt != 4)
                                Console.Write("|");
                        }
                        else
                        {
                            for (int iCpt1 = 1; iCpt1 <= 4; iCpt1++)
                            {
                                Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), ChercheCouleur(5)); // On écrit 4 espace de couleur blanche.
                                Console.Write(" ");
                            }
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(iCpt); // Écrit le chiffre
                            for (int iCpt1 = 1; iCpt1 <= 4; iCpt1++)
                            {
                                Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), ChercheCouleur(5)); // On écrit 4 espace de couleur blanche.
                                Console.Write(" ");
                            }
                            Console.ResetColor();
                            if (iCpt != 4)
                                Console.Write("|");
                        }
                    }
                    Console.WriteLine();
                    Console.ResetColor();
                }
            }
            Console.WriteLine("=======================================");
            if (iCoup <= 4)
                Console.Write("En cours d'animation...");
        }
        /// <summary>
        /// Auteur: Zachary Denis
        /// Description: En gros on fait 255 coups pour le jeu
        /// Date: 2020-11-09
        /// </summary>
        static private void FaireCoup()
        {
            for (int iCpt = 0; iCpt < 255; iCpt++)
                _abyCoup[iCpt] = (byte)_rnd.Next(1, 5);
        }
        /// <summary>
        /// Auteur: Zachary Denis
        /// Description: Méthodes qui sert a faire jouer l'animation.
        /// Data: 2020-11-17
        /// </summary>
        static private void RoulerJeu()
        {
            FaireCoup();
            _bySéquence = 0;
            _sRéponse = "";
            string sVraiSéquence = "";
            bool bValide = false;
            do
            {
                Console.Clear();
                for (int iCpt = 0; iCpt <= _bySéquence; iCpt++)
                {
                    AfficherJeu(_abyCoup[iCpt]);
                    Thread.Sleep(1500);
                }
                sVraiSéquence += _abyCoup[_bySéquence];
                AfficherJeu(6);
                Console.WriteLine("Animation terminée...");
                Console.WriteLine("");
                Console.Write("Entrer votre séquence: ");
                _sRéponse = Console.ReadLine();
                if (_sRéponse == "q") return;
                if (_sRéponse == sVraiSéquence)
                {
                    Console.WriteLine("BRAVO! Vous avez suivi la séquence! On ajoute un tour.");
                    Console.Write("APPUYEZ SUR UNE TOUCHE...");
                    Console.ReadKey();
                    _bySéquence += 1;
                }
                else
                {
                    if (_bySéquence > _byRecord)
                        _byRecord = _bySéquence;
                    Console.WriteLine("ERREUR: Mauvaise séquence!");
                    Console.WriteLine("Nombre de fois trouvé...........: " + _bySéquence);
                    Console.WriteLine("Record du nombre de fois trouvé.:" + _byRecord);
                    bValide = true;
                }
            } while (!bValide);
        }
        #endregion
        static void Main(string[] args)
        {
            Console.Title = "Jeu de Simon"; // Sert a changer le nom du titre de la console
            char cChoix = ' ';
            bool bValide = false;
            AfficherJeu(6);
            Console.Write("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
            RoulerJeu();
            do
            {
                if (_sRéponse == "q")
                {
                    Console.Clear();
                    Console.WriteLine("Au revoir!");
                    return;
                }
                Console.Write("Voulez-vous recommencer (o/n)?: ");
                cChoix = Console.ReadKey().KeyChar;
                if (Char.ToLower(cChoix) == 'o')
                    RoulerJeu();
                else if (_sRéponse == "q" || Char.ToLower(cChoix) == 'n')
                {
                    Console.Clear();
                    Console.WriteLine("Au revoir!");
                    bValide = true;
                }
                else
                    Console.WriteLine("\n\rERREUR: Mauvais choix veuillez réessayer!");
            } while (!bValide);
        }
    }
}
