<h1 class="code-line" data-line-start=0 data-line-end=1 ><a id="FlexiLeaf_0"></a>FlexiLeaf</h1>
<p class="has-line-data" data-line-start="2" data-line-end="3">FlexiLeaf est un projet en développement destiné à créer une application de contrôle à distance d’ordinateur. Le projet est composé de trois éléments principaux :</p>
<ol>
<li class="has-line-data" data-line-start="4" data-line-end="5"><strong>FlexiLeaf.Core</strong> : il s’agit du cœur du projet, qui gère la création, l’envoi et la réception des paquets.</li>
<li class="has-line-data" data-line-start="5" data-line-end="6"><strong>FlexiLeaf.Bridge</strong> : c’est la passerelle qui fait la liaison entre le contrôleur (ControlHub) et l’ordinateur contrôlé (FlexiLeaf.StealthRunner).</li>
<li class="has-line-data" data-line-start="6" data-line-end="7"><strong>ControlHub</strong> : c’est l’interface de contrôle à distance qui donne des instructions à FlexiLeaf.StealthRunner.</li>
<li class="has-line-data" data-line-start="7" data-line-end="9"><strong>FlexiLeaf.StealthRunner</strong> : c’est l’application exécutée sur l’ordinateur contrôlé. Elle exécute les instructions données par ControlHub.</li>
</ol>
<h2 class="code-line" data-line-start=9 data-line-end=10 ><a id="Systme_de_paquets_9"></a>Système de paquets</h2>
<p class="has-line-data" data-line-start="11" data-line-end="12">Dans FlexiLeaf, les informations sont transmises sous forme de paquets. Pour créer un nouveau type de paquet, il suffit de créer une classe qui hérite de la classe <code>Packet</code>. Cette classe doit avoir un constructeur vide, même si elle a d’autres constructeurs.</p>
<p class="has-line-data" data-line-start="13" data-line-end="14">Pour intercepter et traiter les paquets, créez une fonction statique avec le premier argument étant le type de paquet et le deuxième argument étant la connexion (<code>TcpClient</code> ou <code>Client</code>). Cette fonction doit également avoir l’attribut <code>PacketHandler</code>. Voici un exemple d’une telle fonction :</p>
<pre><code class="has-line-data" data-line-start="16" data-line-end="26" class="language-csharp">[PacketHandler]
<span class="hljs-function"><span class="hljs-keyword">public</span> <span class="hljs-keyword">async</span> <span class="hljs-keyword">static</span> <span class="hljs-keyword">void</span> <span class="hljs-title">Click</span>(<span class="hljs-params">MousePacket packet, TcpClient client</span>)
</span>{
    MouseOperations.SetCursorPosition(packet.X, packet.Y);
    <span class="hljs-keyword">if</span> (packet.MouseEvent != MouseEventFlags.Move)
    {
        MouseOperations.MouseEvent(packet.MouseEvent);
    }
}
</code></pre>
<p class="has-line-data" data-line-start="26" data-line-end="27">Les paquets sont automatiquement sérialisés et désérialisés, ce qui signifie que vous n’avez pas à vous soucier de ces détails lorsque vous travaillez avec eux.</p>
<h2 class="code-line" data-line-start=28 data-line-end=29 ><a id="Bridge_28"></a>Bridge</h2>
<p class="has-line-data" data-line-start="30" data-line-end="31">Bridge sert de lien entre les clients Stealth et le contrôleur. Il ouvre deux connexions : une pour les clients Stealth et une pour le contrôleur. Un mot de passe est requis pour que le contrôleur se connecte.</p>
<h2 class="code-line" data-line-start=32 data-line-end=33 ><a id="Utilisation_32"></a>Utilisation</h2>
<p class="has-line-data" data-line-start="34" data-line-end="35">En tant que projet en développement, le processus d’utilisation de FlexiLeaf est en cours de détermination et sera documenté en détail ici une fois finalisé.</p>
<h2 class="code-line" data-line-start=36 data-line-end=37 ><a id="Contributions_36"></a>Contributions</h2>
<p class="has-line-data" data-line-start="38" data-line-end="39">Les contributions sont les bienvenues ! N’hésitez pas à ouvrir une issue pour signaler un bug ou à proposer une amélioration. Si vous souhaitez contribuer au code, veuillez ouvrir une Pull Request.</p>
<h2 class="code-line" data-line-start=40 data-line-end=41 ><a id="Licence_40"></a>Licence</h2>
<p class="has-line-data" data-line-start="42" data-line-end="43">FlexiLeaf est sous licence <a href="https://choosealicense.com/licenses/agpl-3.0/">GNU Affero General Public License v3.0</a>. En résumé, vous pouvez utiliser, modifier et distribuer le code, mais vous ne pouvez pas le sous-licencier ou l’utiliser à des fins commerciales.</p>
