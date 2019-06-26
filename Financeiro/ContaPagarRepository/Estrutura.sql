DROP TABLE ContasPagar;
DROP TABLE ContasReceber;
DROP TABLE Clientes;

CREATE TABLE ContasPagar (
		Id INT PRIMARY KEY IDENTITY(1,1),
        Nome VARCHAR(100),
        Valor decimal(12,2),
        Tipo VARCHAR(50),
		Data_Vencimento DATE
);
CREATE TABLE ContasReceber(
		Id INT PRIMARY KEY IDENTITY(1,1),
        Nome VARCHAR(100),
		Valor DECIMAL(12,2),
        Valor_Recebido DECIMAL(12,2),
        Data_Recebimento DATE,
		Fechada BIT,
);
CREATE TABLE Clientes(
		Id INT PRIMARY KEY IDENTITY(1,1),
        Nome VARCHAR(100),
		CPF VARCHAR(14),
		RG VARCHAR(12),
		Data_Nascimento DATE
);