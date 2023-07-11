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

En tant que projet en d�veloppement, le processus d'utilisation de FlexiLeaf est en cours de d�termination et sera document� en d�tail ici une fois finalis�.

## Contributions

Les contributions sont les bienvenues ! N'h�sitez pas � ouvrir une issue pour signaler un bug ou � proposer une am�lioration. Si vous souhaitez contribuer au code, veuillez ouvrir une Pull Request.

## Licence

FlexiLeaf est sous licence [GNU Affero General Public License v3.0](https://choosealicense.com/licenses/agpl-3.0/). En r�sum�, vous pouvez utiliser, modifier et distribuer le code, mais vous ne pouvez pas le sous-licencier ou l'utiliser � des fins commerciales.
