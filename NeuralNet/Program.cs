using System;
using System.Runtime.CompilerServices;
using System.Xml;

public class NeuralNetwork{
    private double[,] weights;

    enum OPERATION {Multiply, Add, Subtract}

    public NeuralNetwork(){
        Random randomNumber = new Random(1);
        int numberOfInputNodes = 3;
        int numberOfOutputNodes = 1;

        weights =new double[numberOfInputNodes, numberOfOutputNodes];
        for(int i = 0; i < numberOfInputNodes; i++){
            for(int j = 0; j < numberOfOutputNodes; j++){
                weights[i,j] = 2 * randomNumber.NextDouble() - 1;
            }

          
        }

    }

    private double[,] Activate(double[,]matrix, bool isDerivative){
        int numberOfRows = matrix.GetLength(0);
        int numberofColumns = matrix.GetLength(1);
        double[,] result = new double[numberOfRows,numberofColumns];
        for (int row =0; row< numberOfRows; row++){
            for(int column = 0; column< numberofColumns; row++){
                double sigmoIdOutput = result[row,column] = 1/(1+Math.Exp(-matrix[row, column]));
                double derivativesigmoIdOutput = result[row,column] = matrix[row,column] * (1- matrix[row, column]);
                result[row,column] = isDerivative ? derivativesigmoIdOutput : sigmoIdOutput;

            }
        }

        return result;
    }

    public void Train(double[,] trainingInputs, double[,] trainingOutputs, int numberOfIterations){
        for(int iteration = 0; iteration< numberOfIterations; iteration++){
            double[,] output = Think(trainingOutputs);
            double[,] error = PerformOperation(trainingOutputs, output, OPERATION.Subtract);
            double[,] adjustement = DotProduct(Transpose(trainingInputs), performOperation(error, Activate(output, true), OPERATION.Multiply));
            weights = PerformOperation(weights,adjustement,OPERATION.Add);
        }
    }
}  

