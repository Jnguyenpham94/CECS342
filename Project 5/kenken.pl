:- use_module(library(clpfd)).

operator(add).
operator(mult).
operator(sub).
operator(div).
operator(id).

adds_to(X, Y, Z) :-
    Z #= X + Y.

mults_to(X, Y, Z) :-
    Z #= X * Y.

sum_list([X, Y, Z], S) :-
    S #= X + Y + Z.

product_list([X,Y,Z], S) :-
    S #= X * Y * Z.

get_cell(S, [I,J], Val) :-
    nth0('Param1', 'Param2', 'Param3').

cell_values(Cells, S, Values).

target(Y).

cage(add, target, [Rows,Cols]).
cage(mult, target, [Rows,Cols]).
cage(sub, target, [Rows,Cols]).
cage(div, target, [Rows,Cols]).
cage(id, target, [Rows,Cols]).

solve(S, cage) :-
    cage(operator(X), target(Y), [Rows,Cols]),
    %cage(add ; mult ;  sub ; div ; id, target, [X|Y]),
    
    append(Rows, Values),
    Values ins 1..6,
    % maplist/2 corresponds to "all"; it is true if the given predicate function is true
    % when applied to all elements of the given list.
    maplist(all_different, Rows),

    % transpose/2 does a matrix transpose on the puzzle to give a list of
    % columns instead of rows.
    transpose(Rows, Cols),
    maplist(all_different, Cols),

    % The tricky part: doing the "squares". First give each row a name.
    Rows = [A, B, C, D, E, F],
    % Check that rows A, B, C can be grouped 3 at a time to produce	9 different values.
    squares(A, B, C),
    squares(D, E, F),
    %squares(G, H, I),

    maplist(label, Rows).

    squares([], [], []).
    % Given three lists of at least 3 elements, see if the first 3 elements
    % of each list together make a list of 9 different values. Then see if
    % the same is true of the remaining elements of each list.

    squares([A,B,C|T1], [D,E,F|T2]) :-
        all_different([A,B,C,D,E,F]),
        squares(T1, T2).
    
    check_constraint(S, cage).
    check_cage(S, cage) :-
        check_constraint(S, cage(add, Value, Cells)),
        check_constraint(S, cage(mult, Value, Cells)),
        check_constraint(S, cage(sub, Value, Cells)),
        check_constraint(S, cage(div, Value, Cells)),
        check_constraint(S, cage(id, Value, Cells)).

%potential result
/*
[_,_,_,3,5,4],
[_,_,_,_,_,6],
[_,_,2,_,_,1],
[_,_,3,_,_,5],
[3,_,_,_,_,2],
[1,_,6,_,_,3],
*/

/* hmm would this way be better?
sudoku(Puzzle) :-
	% Label each row as A..I.
	Puzzle = [A, B, C, D, E, F],

	% Each row is made of 6 values.
	A = [A1, A2, A3, A4, A5, A6],
	B = [B1, B2, B3, B4, B5, B6],
	C = [C1, C2, C3, C4, C5, C6],
	D = [D1, D2, D3, D4, D5, D6],
	E = [E1, E2, E3, E4, E5, E6],
	F = [F1, F2, F3, F4, F5, F6],

	% Each column is also 6 values, using the variables from the rows.
	Co1 = [A1, B1, C1, D1, E1, F1],
	Co2 = [A2, B2, C2, D2, E2, F2],
	Co3 = [A3, B3, C3, D3, E3, F3],
	Co4 = [A4, B4, C4, D4, E4, F4],
	Co5 = [A5, B5, C5, D5, E5, F5],
	Co6 = [A6, B6, C6, D6, E6, F6],

	% The 9 "squares" are also of 9 values.
	Sq1 = [A1, A2, A3, B1, B2, B3, C1, C2, C3],
	Sq2 = [A4, A5, A6, B4, B5, B6, C4, C5, C6],
	Sq3 = [A7, A8, A9, B7, B8, B9, C7, C8, C9],
	Sq4 = [D1, D2, D3, E1, E2, E3, F1, F2, F3],
	Sq5 = [D4, D5, D6, E4, E5, E6, F4, F5, F6],
	Sq6 = [D7, D8, D9, E7, E8, E9, F7, F8, F9],

	% "ins" forces each element of a list to be in a particular range.
	% It is part of "constraint logic programming".
	A ins 1..6,
	B ins 1..6,
	C ins 1..6,
	D ins 1..6,
	E ins 1..6,
	F ins 1..6,

	% All items in a row must be different.
	all_different(A),
	all_different(B),
	all_different(C),
	all_different(D),
	all_different(E),
	all_different(F),
	% All items in a column must be different.
	all_different(Co1),
	all_different(Co2),
	all_different(Co3),
	all_different(Co4),
	all_different(Co5),
	all_different(Co6),
	% All items in a square must be different.
	all_different(Sq1),
	all_different(Sq2),
	all_different(Sq3),
	all_different(Sq4),
	all_different(Sq5),
	all_different(Sq6),

	% At this point Prolog will report success, but won't give all the values
	% of the unknown variables. "label" will force this.
	label(A),
	label(B),
	label(C),
	label(D),
	label(E),
	label(F),
*/