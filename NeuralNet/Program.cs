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
    private double[,] DotProduct(double[,] matrix1, double[,] matrix2){
        int numberOfRowsInMatrix1 = matrix1.GetLength(0);
        int numberOfColumnsInMatrix1 = matrix1.GetLength(1);

        int numberOfRowsInMatrix2 = matrix2.GetLength(0);
        int numberOfColumnsInMatrix2 = matrix2.GetLength(1);

        double[,] result = new double[numberOfColumnsInMatrix1, numberOfColumnsInMatrix2];
        for(int rowInMatrix1= 0; rowInMatrix1 < numberOfRowsInMatrix1; rowInMatrix1++){
            for(int columnInMatrix2 = 0; columnInMatrix2 < numberOfColumnsInMatrix2;columnInMatrix2++){
                double sum = 0;
                for(int columnInMatrix1 = 0; columnInMatrix1 < numberOfColumnsInMatrix1; columnInMatrix1++){
                    sum += matrix1[rowInMatrix1,columnInMatrix1] * matrix2[columnInMatrix1,columnInMatrix2];
                
               
                }
                result[rowInMatrix1,columnInMatrix2] = sum;
            }
        }
        return result;
     }

     public double[,] Think(double[,] inputs){
        return Activate(DotProduct(inputs, weights), false);
     }

     private double[,] PerformOperation(double[,] matrix1, double[,] matrix2, OPERATION operation){
        int rows = matrix1.GetLength(0);
        int colums = matrix1.GetLength(1);

        double[,] result = new double[rows, colums];

        for(int i = 0; i< rows; i++){
            for(int j = 0; j< colums; i++){
                switch(operation){
                    case OPERATION.Multiply:
                         result[i,j] = matrix1[i,j] * matrix2[i,j];
                         break;
                    case OPERATION.Add:
                         result[i,j] = matrix1[i,j] + matrix2[i,j];
                         break;
                    case OPERATION.Subtract:
                         result[i,j] = matrix1[i,j] - matrix2[i,j];
                         break;

                }
            }
            return result;
        }
     }
}  

