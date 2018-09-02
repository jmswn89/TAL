The program uses CSVHelper library to load csv file to memory. 
- I have addressed the solution that map csv file to ClientRecord.cs
- I have implemented the solution addressed in point 2.
- To calculate Annualised Premium addressed in point 3, I created a class and extend it from ClientRecorrd class.
- The output in point 4 shows all requested values, however, I don't format the annualised premium amount.

To run the file:
Driver.exe insurer.csv

The program created can be improved by passing multiple CSV filenames. Currently, it only supports a single 
CSV files. The class architecture is not applied. If time permitted, I can Dependency Injection to supply some properties.
Some codes in ConvertUsingClassMap are redundant. If time permitted, I can make remove the redundancy.


