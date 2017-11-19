using System;
using System.Collections;
using System.Collections.Generic;


namespace TicTacToe_AI_DataMining
{
    public class AIProcessor
    {

        /*
         Criar o player, valor que simboliza x ou o para o jogador;
         devolver a melhor posição para o usuario jogar;
         verificar a jogada compativel com o player, tipo x só pode retornar espaços com x
        */

        //lista de jogadas conhecidas, lidas da lista minerada
        private List<string[]> knownPlays;


        public AIProcessor(List<string[]> knownPlays)
        {
            this.knownPlays = knownPlays;
        }

        public string[] playTurn(string[] boardSituation, string difficult, string player)
        {
            boardSituation = normalizeBoardSituation(boardSituation);

            switch (difficult)
            {
                case "easy":
                    boardSituation = makeARandomPlay(boardSituation, player);
                    break;

                case "hard":
                    boardSituation = makeTheBestPlay(boardSituation, player);
                    break;
            }

            return boardSituation;
        }

        public int remainingPlays(string[] boardSituation)
        {
            int remainingPlays = 0;

            //procurando o indice de espaços vazios onde posso jogar
            for (int i = 0; i < 9; i++)
            {    
                if (boardSituation[i] == "b")
                {
                    remainingPlays++;
                }
            }

            return remainingPlays;
        }

        public bool hasWinner(string[] boardSituation)
        {
            string player = "x";

            //checking top row
            if (boardSituation[0] == player && boardSituation[1] == player && boardSituation[2] == player)
            {
                return true;
            }

            //checking mid row
            if (boardSituation[3] == player && boardSituation[4] == player && boardSituation[5] == player)
            {
                return true;
            }

            //checking bot row
            if (boardSituation[6] == player && boardSituation[7] == player && boardSituation[8] == player)
            {
                return true;
            }

            //checking first column
            if (boardSituation[0] == player && boardSituation[3] == player && boardSituation[6] == player)
            {
                return true;
            }

            //checking second column
            if (boardSituation[1] == player && boardSituation[4] == player && boardSituation[7] == player)
            {
                return true;
            }

            //checking third column
            if (boardSituation[2] == player && boardSituation[5] == player && boardSituation[8] == player)
            {
                return true;
            }

            //checking diagonal
            if (boardSituation[0] == player && boardSituation[4] == player && boardSituation[8] == player)
            {
                return true;
            }

            //checking diagonal
            if (boardSituation[2] == player && boardSituation[4] == player && boardSituation[6] == player)
            {
                return true;
            }


            player = "o";
            //checking top row
            if (boardSituation[0] == player && boardSituation[1] == player && boardSituation[2] == player)
            {
                return true;
            }

            //checking mid row
            if (boardSituation[3] == player && boardSituation[4] == player && boardSituation[5] == player)
            {
                return true;
            }

            //checking bot row
            if (boardSituation[6] == player && boardSituation[7] == player && boardSituation[8] == player)
            {
                return true;
            }

            //checking first column
            if (boardSituation[0] == player && boardSituation[3] == player && boardSituation[6] == player)
            {
                return true;
            }

            //checking second column
            if (boardSituation[1] == player && boardSituation[4] == player && boardSituation[7] == player)
            {
                return true;
            }

            //checking third column
            if (boardSituation[2] == player && boardSituation[5] == player && boardSituation[8] == player)
            {
                return true;
            }

            //checking diagonal
            if (boardSituation[0] == player && boardSituation[4] == player && boardSituation[8] == player)
            {
                return true;
            }
            
            //checking diagonal
            if (boardSituation[2] == player && boardSituation[4] == player && boardSituation[6] == player)
            {
                return true;
            }

            return false;
        }

        /**
         *Retorna a posicao para ganhar ou -1 se nao for possivel ganhar
         *
         */
        private int whereToWin(string[] boardSituation, string player)
        {
            int i = 0;
            int playCount = 0;

            //variaveis para colunas
            int column = 0, currentPosition = 0;

            column = 0;
            //verificando as linhas
            for (i = 0; i < 9; i++)
            {  
                //se a linha for multiplo de 3 é uma nova linha
                if(i % 3 == 0)
                {
                    if (i > 0)
                        column += 3;
                    playCount = 0;
                }

                //se a jogada anterior for do oponente ou nao for espaço em branco
                if (!(playCount > -1) || boardSituation[i] != "b")
                {
                    //se a jogada for do oponente
                    if(boardSituation[i] != player)
                    {
                        //sinaliza que a jogada é do oponente, portanto sem chance de vitoria nesta linha, continuar
                        //para a proxima
                        playCount = -1;
                        continue;
                    }                    
                }

                if (boardSituation[i] == player)
                {
                    playCount++;
                }

                //se o proximo elemento pertencer a uma nova linha verifico a condição de vitoria dessa linha
                //if(i % 3 == 2 && playCount == 2)
                if (playCount == 2)
                {
                    //percorre do começo ao fim da linha procurando o espaço vazio, portanto o que sinaliza a vitoria
                    for(int j = column; j < column+3; j++)
                    {
                        if(boardSituation[j] == "b")
                        {
                            return j;
                        }
                    }
                }
            }

            //verificando as colunas
            //o proximo espaço da coluna é igual ao espaço atual + 3, ex: estou na posição 0, 0 + 3 = 3, a proxima
            //é pos 3.
            //sao 3 colunas ao todo
            for (i = 0; i < 3; i++)
            {
                //mudando a coluna
                column = i;
                currentPosition = i;
                playCount = 0;


                for (int j = 0; j < 3; j++)
                {
                    //se a jogada anterior for do oponente ou nao for espaço em branco
                    if (!(playCount > -1) || boardSituation[currentPosition] != "b")
                    {
                        //se a jogada for do oponente
                        if (boardSituation[currentPosition] != player)
                        {
                            //sinaliza que a jogada é do oponente, portanto sem chance de vitoria nesta coluna, continuar
                            //para a proxima
                            playCount = -1;
                            continue;
                        }
                    }

                    if (boardSituation[currentPosition] == player)
                    {
                        playCount++;
                    }

                    //se o proximo elemento pertencer a uma nova coluna verifico a condição de vitoria dessa coluna
                    if (j % 3 == 2 && playCount == 2)
                    {
                        //vou para o começo da coluna
                        currentPosition = i;

                        //percorre do começo ao fim da coluna procurando o espaço vazio, portanto o que sinaliza a vitoria
                        for (j = 0; j < 3; j++)
                        {                            
                            if (boardSituation[currentPosition] == "b")
                                return currentPosition;
                            currentPosition += 3;
                        }
                    }

                    currentPosition += 3;
                }
            }
            
            //verificando diagonais
            currentPosition = 0;
            playCount = 0;
            for (i = 0; i < 3; i++)
            {
                if(boardSituation[currentPosition] == player)
                {
                    playCount++;
                }

                //ultima posicao da coluna
                if(currentPosition == 8 && playCount >=2)
                {
                    //reinicia o laço
                    i = 0;
                    currentPosition = 0;

                    if (boardSituation[currentPosition] == "b")
                        return currentPosition;
                }

                //variação das colunas, sao posiçoes 0, 4 e 8
                currentPosition += 4;
            }

            //verificando diagonais
            currentPosition = 0;
            playCount = 0;
            for (i = 0; i < 3; i++)
            {
                if (boardSituation[currentPosition] == player)
                {
                    playCount++;
                }

                //ultima posicao da coluna
                if (currentPosition == 6 && playCount >= 2)
                {
                    //reinicia o laço
                    i = 0;
                    currentPosition = 0;

                    if (boardSituation[currentPosition] == "b")
                        return currentPosition;
                }

                //variação das colunas, sao posiçoes 2, 4 e 6
                currentPosition += 2;
            }


            return -1;
        }

        private string[] makeARandomPlay(string[] boardSituation, string player)
        {
            int randomPositionToPlay = findARandomPositionToPlay(boardSituation);
            boardSituation[randomPositionToPlay] = player;

            return boardSituation;
        }

        private string[] makeTheBestPlay(string[] boardSituation, string player)
        {
            double playRate = 1000, tempPlayRate = 0;
            int whereToPlay = -1;

            List<string[]> compatiblePlays = getCompatiblePlays(boardSituation);
            compatiblePlays = filterByWinPlays(compatiblePlays, player);
            List<int> emptyPlaces = getEmptyPlaces(boardSituation);

            /*
             Verificar se a vitoria é possivel, se for entao ganhe, se nao:
             Para cada espaço vazio calcular o número de vezes que a jogada foi feita no espaço;
             Achar o espaço com maior número de jogadas vitoriosas;
             Marcar a jogada no melhor espaço.
             */

            int letsWin = whereToWin(boardSituation, player);
            if (letsWin > -1)
            {
                boardSituation[letsWin] = player;
                return boardSituation;
            }

            //se nao houverem espaços vazios, nao jogue
            if (emptyPlaces.Count < 1)
            {
                return boardSituation;
            }

            for (int i = 0; i < emptyPlaces.Count; i++)
            {     
                //se nao houverem jogadas compativeis, jogue no primeiro espaço vazio
                if (compatiblePlays.Count < 1)
                {
                    whereToPlay = emptyPlaces[0];
                    break;
                }
                
                tempPlayRate = calculateAppearanceRate(emptyPlaces[i], compatiblePlays, player);

                if (tempPlayRate < playRate)
                {
                    playRate = tempPlayRate;
                    whereToPlay = emptyPlaces[i];
                }
            }

            boardSituation[whereToPlay] = player;

            return boardSituation;
        }

        private int findARandomPositionToPlay(string[] boardSituation)
        {
            System.Random rnd = new System.Random();

            List<int> possiblePlays = new List<int>();

            //procurando o indice de espaços vazios onde posso jogar
            for (int i = 0; i < 9; i++)
            {
                if (boardSituation[i] == "b")
                {
                    possiblePlays.Add(i);
                }
            }

            int randomPosition = rnd.Next(0, possiblePlays.Count);

            return possiblePlays[randomPosition];
        }

        private string[] normalizeBoardSituation(string[] boardSituation)
        {
            for (int i = 0; i < 9; i++)
            {
                if (!(boardSituation[i] == "x" || boardSituation[i] == "o" || boardSituation[i] == "b"))
                {
                    boardSituation[i] = "b";
                }
            }

            return boardSituation;
        }

        private List<string[]> getCompatiblePlays(string[] boardSituation)
        {
            bool isCompatible = true;
            List<string[]> compatiblePlays = new List<string[]>();

            for (int i = 0; i < knownPlays.Count; i++)
            {
                isCompatible = true;

                for (int j = 0; j < 9; j++)
                {
                    //se a possição estiver vazia considere compatível
                    if (boardSituation[j] == "b") continue;

                    if (boardSituation[j] != knownPlays[i][j])
                    {
                        isCompatible = false;
                        break;
                    }
                }

                if (isCompatible == true)
                {
                    compatiblePlays.Add(knownPlays[i]);
                }
            }

            return compatiblePlays;
        }

        private List<string[]> filterByWinPlays(List<string[]> listOfPlays, string player)
        {
            //se o player é x entao pego o status positive que na lista quer dizer que ele venceu, se o player for o pego o negative, com 
            //isso x ganha se status for positive e o ganha se status for negative
            string status = player == "x" ? "positive" : "negative";

            //filtrando a lista, retiranddo as jogadas que resultaram em derrotas
            for (int i = 0; i < listOfPlays.Count;)
            {
                if (listOfPlays[i][9] != status)
                {
                    listOfPlays.RemoveAt(i);
                }
                else
                {
                    i++; //incrementar só quando não remover item da lista
                }
            }

            return listOfPlays;
        }

        private List<int> getEmptyPlaces(string[] boardSituation)
        {
            List<int> emptyPlaces = new List<int>();

            for (int i = 0; i < boardSituation.Length; i++)
            {
                if (boardSituation[i] == "b")
                    emptyPlaces.Add(i);
            }

            return emptyPlaces;
        }

        /*
            Calcula o numero de vezes que a jogada aparece baseado na contagem dos dados de jogadas anteriores 
        */
        private double calculateAppearanceRate(int whereToPlay, List<string[]> listOfPlays, string player)
        {
            double xCount = 0, avaliatedCount = 0;

            foreach (string[] play in listOfPlays)
            {
                //se for um espaço vazio, pule a operação
                if (play[whereToPlay] == "b")
                    continue;

                //contabiliza a jogada que foi avaliada
                avaliatedCount++;
                if (play[whereToPlay] == "x")
                    xCount++;
            }

            if (player == "x")
                return ((avaliatedCount / xCount) * 10.0);

            //se player for o
            return (100 - ((avaliatedCount / xCount) * 10));
        }
    }
}