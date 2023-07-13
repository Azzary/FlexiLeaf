# FlexiLeaf

FlexiLeaf est un projet en d�velopPement Destin� � cr�er une application de contr�le � distance d'ordinateur. Le projet est compos� de trois �l�ments principaux :

1. **FlexiLeaf.Core** : il s'agit du c�ur du projet, qui g�re la cr�ation, l'envoi et la r�ception des paquets.
2. **FlexiLeaf.Bridge** : c'est la passerelle qui fait la liaison entre le contr�leur (ControlHub) et l'ordinateur contr�l� (FlexiLeaf.StealthRunner).
3. **ControlHub** : c'est l'interface de contr�le � distance qui donne des instructions � FlexiLeaf.StealthRunner.
4. **FlexiLeaf.StealthRunner** : c'est l'application ex�cut�e sur l'ordinateur contr�l�. Elle ex�cute les instructions donn�es par ControlHub.

## Syst�me de paquets

Dans FlexiLeaf, les informations sont transmises sous forme de paquets. Pour cr�er un nouveau type de paquet, il suffit de cr�er une classe qui h�rite de la classe `Packet`. Cette classe doit avoir un constructeur vide, m�me si elle a d'autres constructeurs.

Pour intercepter et traiter les paquets, cr�ez une fonction statique avec le premier argument �tant le type de paquet et le deuxi�me argument �tant la connexion (`TcpClient` ou `Client`). Cette fonction doit �galement avoir l'attribut `PacketHandler`. Voici un exemple d'une telle fonction :

```csharp
[PacketHandler]
public async static void Click(MousePacket packet, TcpClient client)
{
    MouseOperations.SetCursorPosition(packet.X, packet.Y);
    if (packet.MouseEvent != MouseEventFlags.Move)
    {
        MouseOperations.MouseEvent(packet.MouseEvent);
    }
}
```
Les paquets sont automatiquement s�rialis�s et d�s�rialis�s, ce qui signifie que vous n'avez pas � vous soucier de ces d�tails lorsque vous travaillez avec eux.

## Bridge

Bridge sert de lien entre les clients Stealth et le contr�leur. Il ouvre deux connexions : une pour les clients Stealth et une pour le contr�leur. Un mot de passe est requis pour que le contr�leur se connecte.

## Utilisation

Lancer Bridge sur un serveur
ControlHub sur l'ordinateur mere
StealthRunner sur l'ordinateur cible

## Contributions

Les contributions sont les bienvenues ! N'h�sitez pas � ouvrir une issue pour signaler un bug ou � proposer une am�lioration. Si vous souhaitez contribuer au code, veuillez ouvrir une Pull Request.

## Licence

FlexiLeaf est sous licence [GNU Affero General Public License v3.0](https://choosealicense.com/licenses/agpl-3.0/). En r�sum�, vous pouvez utiliser, modifier et distribuer le code, mais vous ne pouvez pas le sous-licencier ou l'utiliser � des fins commerciales.

## Fonctionnalit�s

FlexiLeaf offre un ensemble de fonctionnalit�s puissantes qui vous permettent de contr�ler � distance un ordinateur avec facilit�. Voici un aper�u des principales fonctionnalit�s disponibles :

1. **Changement de client cible** : Permet de changer facilement l'ordinateur cible que vous contr�lez. Cette fonctionnalit� est particuli�rement utile pour g�rer plusieurs machines � distance.

2. **Visualisation de l'�cran de la cible** : Vous permet de voir l'�cran de l'ordinateur cible en temps r�el. Cela vous offre une visibilit� compl�te sur les op�rations que vous effectuez.

3. **Contr�le de la souris** : Donne la capacit� de d�placer la souris sur l'ordinateur cible. Vous pouvez �galement effectuer des clics gauche, droit et avec la molette de la souris.

4. **Gestionnaire de fichiers** : Cette fonctionnalit� vous permet de naviguer dans la structure de fichiers de l'ordinateur cible. Vous pouvez cr�er de nouveaux dossiers et envoyer des fichiers � l'ordinateur cible. Il affiche �galement la structure de l'arborescence des dossiers et fichiers pour une navigation facile.

5. **Commandes** : Dispose d'une fonctionnalit� de ligne de commande puissante qui vous permet d'ouvrir une console cach�e sur l'ordinateur cible, d'ex�cuter des commandes, et de recevoir tous les outputs de celle-ci.

6. **Gestion des processus** : Affiche la liste des processus en cours d'ex�cution sur l'ordinateur cible, y compris le nom, le PID et plus tard le pourcentage d'utilisation du CPU, le pourcentage d'utilisation de la RAM et le pourcentage d'utilisation du r�seau. Vous pouvez fermer les processus manuellement ou les ajouter � la liste KillOnSpawn pour les tuer automatiquement d�s leur lancement.

## To-Do Liste

1. **Gestionnaire de fichiers - Clic droit** : Ajouter un menu contextuel (clic droit) dans le gestionnaire de fichiers. Ce menu doit offrir les options pour supprimer, renommer, etc. un fichier s�lectionn�.

2. **R�cup�ration de fichiers** : Ajouter une fonctionnalit� permettant de r�cup�rer des fichiers pr�sents sur l'ordinateur cible. Cette fonctionnalit� devrait permettre de t�l�charger des fichiers de l'ordinateur cible vers l'ordinateur h�te.

3. **Capture d'�cran - Qualit� du streaming** : Pour la fonctionnalit� de capture d'�cran, ajouter la possibilit� de r�gler la qualit� du streaming. Cela pourrait inclure des r�glages pour le taux de rafra�chissement (frame rate) et la qualit� de l'�cran (r�solution, profondeur de couleur, etc.).

## Clause de non-responsabilit�

Veuillez noter que l'utilisation de ce logiciel est � vos propres risques. L'auteur ne peut �tre tenu responsable des dommages ou pr�judices d�coulant de l'utilisation de ce logiciel. Bien que tous les efforts aient �t� faits pour garantir la fiabilit� et l'exactitude de ce logiciel, l'auteur ne fait aucune d�claration ni garantie quant � son utilit� ou son ad�quation � un usage particulier.
Il est recommand� d'utiliser ce logiciel conform�ment aux lois en vigueur et de respecter les droits d'autrui. L'auteur d�cline toute responsabilit� pour toute utilisation abusive, ill�gale ou non autoris�e de ce logiciel.


