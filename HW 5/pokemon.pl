% Simple facts.

number(pikachu, 25).
evolves(pikachu, raichu).
evolves(charmander, charmeleon).
evolves(charmeleon, charizard).
evolves(eevee, jolteon).
evolves(eevee, flareon).
evolves(eevee, vaporeon).
evolves(charmander, charmeleon, level(16)).
evolves(charmeleon, charizard, level(36)).
evolves(eevee, jolteon, item(thunderstone)).
evolves(eevee, flareon, item(firestone)).
evolves(eevee, vaporeon, item(waterstone)).

% Slightly more complex facts.

move(thunderbolt, electric, special, 90).
move(thunderpunch, electric, physical, 75).
learns(pikachu, thunderbolt, level(36)). % Pikachu learns Thunderbolt at level 36.
learns(pikachu, thunderpunch, tm(5)).

% Simple rules.

sibling(X, Y) :- evolves(Parent, X), evolves(Parent, Y), X \= Y. % the comma means "and". "\=" means "does not unify".

canUseItem(Pokemon, tm(X)) :- learns(Pokemon, _, tm(X)). % _ is "don't care", yet again.


% A rule with multiple clauses.
descendent(X, Y) :- evolves(Y, X).
descendent(X, Y) :- evolves(Y, Z), descendent(X, Z). % This one is recursive!!

effective(bug, dark).
effective(bug, grass).
effective(bug, psychic).
effective(dark, psychic).
effective(dark, ghost).
effective(dragon, dragon).
effective(electric, water). % electric is effective against water
effective(electric, flying). % electric is effective against flying
effective(fairy, dark).
effective(fairy, dragon).
effective(fairy, fighting).
effective(fighting, dark). % fighting is effective against dark.
effective(fighting, ice). % fighting is effective against ice.
effective(fighting, normal). % fighting is effective against normal.
effective(fighting, rock). % fighting is effective against rock.
effective(fighting, steel). % fighting is effective against steel.
effective(fire, bug).
effective(fire, grass).
effective(fire, ice).
effective(fire, steel).
effective(flying, bug).
effective(flying, fighting).
effective(flying, grass).
effective(ghost, psychic). %ghost is effective against psychic
effective(ghost, ghost). %ghost is effective against ghosts
effective(grass, rock).
effective(grass, water).
effective(grass, ground).
effective(ground, electric). % ground is effective against electric.
effective(ground, fire ). % ground is effective against fire.
effective(ground, poison). % ground is effective against poison.
effective(ground, rock). % ground is effective against rock.
effective(ground, steel). % ground is effective against steel.
effective(ice, dragon).
effective(ice, flying).
effective(ice, grass).
effective(ice, ground).
effective(poison, fairy).
effective(poison, grass).
effective(psychic, fighting).
effective(psychic, poison).
effective(rock, bug).
effective(rock, fire).
effective(rock, flying).
effective(rock, ice).
effective(steel, fairy).
effective(steel, ice).
effective(steel, rock).
effective(water, fire).
effective(water, rock).
effective(water,ground).
immune(dragon, fairy).
immune(electric, ground). % ground is immune against electric
immune(fighting, ghost). % fighting is immune against ghost.
immune(ghost, normal). % normal is immune to ghost
immune(ground, flying). % ground does no damage to flying types.
immune(normal, ghost).
immune(poison, steel).
immune(psychic, dark).
ineffective( grass, fire).
ineffective(bug, fairy).
ineffective(bug, fighting).
ineffective(bug, fire).
ineffective(bug, flying).
ineffective(bug, ghost).
ineffective(bug, poison).
ineffective(bug, steel).
ineffective(dark, dark).
ineffective(dark, fairy).
ineffective(dark, fighting).
ineffective(dragon, steel).
ineffective(electric, dragon). % electric is ineffective against dragon
ineffective(electric, electric). % electric is ineffective against electric
ineffective(electric, grass). % electric is ineffective against grass
ineffective(fairy, fire).
ineffective(fairy, poison).
ineffective(fairy, steel).
ineffective(fighting,bug). %fighting is ineffective against bug
ineffective(fighting,fairy). %fighting is ineffective against fairy
ineffective(fighting,flying). %fighting is ineffective against flying
ineffective(fighting,poison). %fighting is ineffective against poison
ineffective(fighting,psychic). %fighting is ineffective against psychic
ineffective(fire, dragon).
ineffective(fire, fire).
ineffective(fire, rock).
ineffective(fire, water).
ineffective(flying, electric).
ineffective(flying, rock).
ineffective(flying, steel).
ineffective(ghost, dark). %ghost is ineffective against dark
ineffective(grass, bug).
ineffective(grass, dragon).
ineffective(grass, fly).
ineffective(grass, steel).
ineffective(grass, grass).
ineffective(grass, poison).
ineffective(ground, bug). % ground is ineffective against bug.
ineffective(ground, grass). % ground is ineffective against grass.
ineffective(ice, fire).
ineffective(ice, ice).
ineffective(ice, steel).
ineffective(ice, water).
ineffective(normal, rock).
ineffective(normal, steel).
ineffective(poison, ghost).
ineffective(poison, ground).
ineffective(poison, poison).
ineffective(poison, rock).
ineffective(psychic, psychic).
ineffective(psychic, steel).
ineffective(rock, fighting).
ineffective(rock, ground).
ineffective(rock, steel).
ineffective(steal, electric).
ineffective(steel, fire).
ineffective(steel, steel).
ineffective(steel, water).
ineffective(water, dragon).
ineffective(water, grass).
ineffective(water, water).

damageMultiplier(MoveType, TargetType, 2.0) :- effective(MoveType, TargetType). % a move does 2x damage against a target if it is effective against that target.
damageMultiplier(MoveType, TargetType, 0.5) :- ineffective(MoveType, TargetType). % a move does 0.5 damage against a target if it is not very effective against that target.
damageMultiplier(MoveType, TargetType, 0) :- immune(MoveType, TargetType). % a move does 0x damage against a target if it is immune against that target.
damageMultiplier(MoveType, TargetType, 1.0). % a move does 1x damage against a target if it is normal against that target; this case catches everything else after the above 3

%attackEffectiveness(fire, [water, grass], X) gives X=...what? 1
%attackEffectiveness(fire, [electric, normal], X) gives X=...? 1
%attackEffectiveness(fire, [grass, ice, steel], X) gives X=...?  8
%attackEffectiveness(M, [water, flying], 4.0) gives M=...? electric
%attackEffectiveness(grass, [water, T], 1.0) gives T=...? fire, grass, poison...

product(X, Y, Z) :- Z is X * Y.
attackEffectiveness(MoveType, TargetType, Z) :- damageMultiplier(MoveType,TargetType,Z).
attackEffectiveness(MoveType, TargetType, Z) :- foldl(product, maplist(damageMultiplier(MoveType), TargetType, Z), 1.0, Z).