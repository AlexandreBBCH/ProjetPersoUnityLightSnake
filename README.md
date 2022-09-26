# Documentation Light Snake

## 1. Titre
Light Snake (Provisoire)

## 2. Matériel et logiciels à disposition
### Matériel
1. Un Post fonctionnel 

### Logiciel
1. Visual Studio code
2. Unity

## 3. Prérequis 
1. Connaissance en C#
2. Extension visual studio pour Unity


## 4. Descriptif complet du projet
### Descriptif de Light Snake :

### -> Ou que le joueur soit
  - Le joueur peut appuyer sur "esc" ouvrant les option général
    - Les options général contient la gestion audio ainsi qu'un indication des touche de jeu.

### -> Le joueur à accès à un menu principal au lancement du jeu

- Un menu disposant de 4 boutons

    - (Solo servant à lancer le mode solo du jeu.)
    - Multijoueur servant à lancer le mode multijoueur.
    - Quitter servant à quitter le jeu .
    - Un engrenage servant à ouvrir le menu des options général de jeu.

- Si l'utilisateur clique sur Solo
    - Le joueur sera redirigé dans le menu solo

- Si l'utilisateur clique sur Multijoueur 
    - Le joueur sera redirigé sur le menu multijoueur

### (-> Menu Solo)
- (Le joueur disposera d'une carte permettant de naviguer dans les niveaux de la campagne du jeu. (à définir))

    
### -> Menu de selection de mode de Jeu multijoueur
- Le joueur peut revenir en arrière en cliquant sur le bouton "retour"
- Si l'utilisateur clique sur l'un d'eux
    - Le joueur est redirigié sur les paramètre du mode de jeu concerné (nombre de joueur, Objet, Evenement et d'autre dépendant du mode de jeu)

 
 ### -> Menu paramètrage de mode de Jeu et de lancement
- Bouton lancement du jeu
- Paramètre généraux (nombre joueurs et durée) représenté à l'aide d'un scroll bar. 
- Bouton paramètre moins important


### -> Control Généraux et Concordance entre les jeu
  - Le joueur produit en se déplaçant un mur solide.
  - Le joueur ne peut pas s'arrêter.
  - Le joueur peut se déplacé dans les 4 directions avec les touches associé.
  - Le joueur peut tiré des missiles détruisant le mur placé ou le joueur ennemie. 
  - Le joueur peut se propulsé (boost) en maintenant la touche de tir
  - Le joueur peut ramasser des objets alternant divers aspect du jeu.
  - Le joueur peut ramasser divers objets.

### ( -> Mode Solo )
- ( Principe )
    - ( Un mode basé sur une résolution d'énigme )

### -> Mode de jeu multijoueur (règle)

- Mode de jeu "Round"
   - Un mode de jeu régis par un nombre de round
   - A la fin du jeu , le joueur ayant remporter le plus de partie remporte le jeu
  
- Mode de jeu "Vie"
    - Un mode de jeu régis par un nombre de vie de chaque joueur
    - Les joueurs qui auront survecue le plus longtemps remporte la partie
    - Durant les parties le joueur peut se disputer des vie apparaissant durant le jeu

- Mode de jeu "Survie"
    - Un mode de jeu régis par le temps
    - Il y a deux type de joueur
      - Le "chat" qui doit vaincre la souris avant la fin du temps impartie. Il produit des murs
      - La "Souris" qui doit s'enfuir jusqu'a la fin du temps impartie. Elle ne produit pas de mur.
    - A la fin du temps les "souris" gagnent. Si les "souris" meurt avant le temps impartie , le(s) "chats" gagnent.

### -> Amélioration possible (si le temps me le permet)(classé par importance)
1. Implementer un mode multi Online 
2. Implementer un mode solo d'enigmes basé sur la méchanique du snake (posage de mur)
3. Implementer des IA rendant le mode local jouable en solo
4. Ajout d'evenement dans le mode multijoueur
5. Ajout de map aléatoire (obsactle prefais ou aléatoire)


## 5. Livrables
1. Planning
2. Trello
3. Rapport de projet
4. Manuel d'utilisateur
5. Git

