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

## Utilisation

Lancer Bridge sur un serveur
ControlHub sur l'ordinateur mere
StealthRunner sur l'ordinateur cible

## Contributions

Les contributions sont les bienvenues ! N'hésitez pas à ouvrir une issue pour signaler un bug ou à proposer une amélioration. Si vous souhaitez contribuer au code, veuillez ouvrir une Pull Request.

## Licence

FlexiLeaf est sous licence [GNU Affero General Public License v3.0](https://choosealicense.com/licenses/agpl-3.0/). En résumé, vous pouvez utiliser, modifier et distribuer le code, mais vous ne pouvez pas le sous-licencier ou l'utiliser à des fins commerciales.

## Fonctionnalités

FlexiLeaf offre un ensemble de fonctionnalités puissantes qui vous permettent de contrôler à distance un ordinateur avec facilité. Voici un aperçu des principales fonctionnalités disponibles :

1. **Changement de client cible** : Permet de facilement changer l'ordinateur cible que vous contrôlez. Cette fonctionnalité est particulièrement utile pour gérer plusieurs machines à distance.

2. **Visualisation de l'écran de la cible** : Pouvoir voir l'écran de l'ordinateur cible en temps réel. Cela vous donne une visibilité complète sur les opérations que vous effectuez.

3. **Contrôle de la souris** : Donne la capacité de bouger la souris sur l'ordinateur cible. Vous pouvez également effectuer des clics gauche, droit et de la molette de la souris.

4. **Envoi de fichiers** : Permet d'envoyer un ou plusieurs fichiers, quel que soit leur volume, sur l'ordinateur cible.

5. **Commandes** : Dispose d'une fonctionnalité de ligne de commande puissante qui vous permet d'ouvrir une console cachée sur l'ordinateur cible, d'exécuter des commandes, et de recevoir touts les output celle ci.

6. **Process Management** : ProcessManagement : Affiche la liste des processus en cours d'exécution sur l'ordinateur cible, y compris le nom, le PID ( plutart le pourcentage d'utilisation du CPU, le pourcentage d'utilisation de la RAM et le pourcentage d'utilisation du réseau). Vous pouvez fermer les processus manuellement ou les ajouter à la liste KillOnSpawn pour les tuer automatiquement dès leur lancement.
FlexiLeaf est en constante évolution, avec de nouvelles fonctionnalités ajoutées.

## Clause de non-responsabilité

Veuillez noter que l'utilisation de ce logiciel est à vos propres risques. L'auteur ne peut être tenu responsable des dommages ou préjudices découlant de l'utilisation de ce logiciel. Bien que tous les efforts aient été faits pour garantir la fiabilité et l'exactitude de ce logiciel, l'auteur ne fait aucune déclaration ni garantie quant à son utilité ou son adéquation à un usage particulier.
Il est recommandé d'utiliser ce logiciel conformément aux lois en vigueur et de respecter les droits d'autrui. L'auteur décline toute responsabilité pour toute utilisation abusive, illégale ou non autorisée de ce logiciel.