# Jeu de HEX

## Scénario

Le jeu de “Hex” est un jeu de société stratégique pour deux joueurs. Il a été inventé par le mathématicien danois Piet Hein en 1942 et redécouvert indépendamment par le mathématicien et physicien John Nash dans les années 1940. Le jeu se joue sur un plateau en forme de losange composé de hexagones. La taille standard du plateau est de 11x11, mais il peut varier.


![image](https://github.com/IeM-P8/Hex-py/assets/85486775/48b020b9-5555-4aa7-a821-3deb327b52c5)

Chaque joueur possède des pions d'une couleur spécifique. Les joueurs placent alternativement un de leurs pions sur un hexagone vide du plateau. L'objectif du jeu est de former un chemin continu reliant les deux côtés opposés du plateau marqués par la couleur du joueur. Le premier joueur à réussir à créer un tel chemin gagne la partie.

![image](https://github.com/IeM-P8/Hex-py/assets/85486775/92f0d04a-d3f4-49e4-8fbd-1b2c47808c4f)

Ce qui rend “Hex” intéressant, c'est qu'il est impossible qu'il y ait un match nul ; l'un des deux joueurs gagnera inévitablement. Cela est dû à la structure du plateau et aux règles de jeu. Hex est également connu pour sa profondeur stratégique. Bien qu'il soit simple à apprendre, il offre une complexité et une variété de stratégies importante.
“Hex” est non seulement un jeu divertissant mais aussi un sujet d'étude dans les domaines de la théorie des jeux et de l'intelligence artificielle. Il représente un défi intéressant pour les algorithmes d'IA en raison de sa simplicité apparente et de sa complexité sous-jacente.

## Objectif

## Rapport technique

# Processus d'apprentissage
![image](https://github.com/IeM-P8/Hex-py/blob/main/imgs/board_7x7.png)


Le processus processus d’apprentissage avec deux IA vierges qui joue l’une contre l’autre.

Ce processus permettra de générer un très grand nombre de parties très rapidement sans attendre qu’un joueur humain joue. En comparaison avec un apprentissage humain VS IA, la courbe d’apprentissage risque d’être plus faible au début mais permettra de développer des stratégies sans être limité par les biais cognitifs humain. D’un autre côté, elle ne pourra pas utiliser ces biais pour générer des stratégies.

Une fois un très grand nombre de parties de simulés, nous pourront l’utiliser pour jouer contre elle.

## Création du modèle (MLP)

Pour générer notre MLP (MultiLayer Perceptron) nous utilisons le logicel JNNET 1.3.2



## Rapport d'utilisation
