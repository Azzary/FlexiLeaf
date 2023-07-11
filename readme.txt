# FlexiLeaf

FlexiLeaf est un projet en dévelopPement Destiné à créer une application de contrôle à distance d'ordinateur. Le projet est composé de trois éléments principaux :

1. **FlexiLeaf.Core** : il s'agit du cœur du projet, qui gère la création, l'envoi et la réception des paquets.
2. **FlexiLeaf.Bridge** : c'est la passerelle qui fait la liaison entre le contrôleur (ControlHub) et l'ordinateur contrôlé (FlexiLeaf.StealthRunner).
3. **ControlHub** : c'est l'interface de contrôle à distance qui donne des instructions à FlexiLeaf.StealthRunner.
4. **FlexiLeaf.StealthRunner** : c'est l'application exécutée sur l'ordinateur contrôlé. Elle exécute les instructions données par ControlHub.

## Système de paquets

Dans FlexiLeaf, les informations sont transmises sous forme de paquets. Pour créer un nouveau type de paquet, il suffit de créer une classe qui hérite de la classe `Packet`. Cette classe doit avoir un constructeur vide, même si elle a d'autres constructeurs.

Pour intercepter et traiter les paquets, créez une fonction statique avec le premier argument étant le type de paquet et le deuxième argument étant la connexion (`TcpClient` ou `Client`). Cette fonction doit également avoir l'attribut `PacketHandler`. Voici un exemple d'une telle fonction :

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
Les paquets sont automatiquement sérialisés et désérialisés, ce qui signifie que vous n'avez pas à vous soucier de ces détails lorsque vous travaillez avec eux.

## Bridge

Bridge sert de lien entre les clients Stealth et le contrôleur. Il ouvre deux connexions : une pour les clients Stealth et une pour le contrôleur. Un mot de passe est requis pour que le contrôleur se connecte.

##Utilisation

En tant que projet en développement, le processus d'utilisation de FlexiLeaf est en cours de détermination et sera documenté en détail ici une fois finalisé.

## Contributions

Les contributions sont les bienvenues ! N'hésitez pas à ouvrir une issue pour signaler un bug ou à proposer une amélioration. Si vous souhaitez contribuer au code, veuillez ouvrir une Pull Request.

## Licence

FlexiLeaf est sous licence [GNU Affero General Public License v3.0](https://choosealicense.com/licenses/agpl-3.0/). En résumé, vous pouvez utiliser, modifier et distribuer le code, mais vous ne pouvez pas le sous-licencier ou l'utiliser à des fins commerciales.
