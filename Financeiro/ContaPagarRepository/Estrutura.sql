DROP TABLE Contas_Pagar;
DROP TABLE Contas_Receber;
DROP TABLE Clientes;

CREATE TABLE Contas_Pagar (
		dd INT PRIMARY KEY IDENTITY(1,1),
        nome VARCHAR(100),
        valor decimal(12,2),
        tipo VARCHAR(50),
		data_Vencimento DATE
);
CREATE TABLE Contas_Receber(
		id INT PRIMARY KEY IDENTITY(1,1),
        nome VARCHAR(100),
		valor DECIMAL(12,2),
        valor_Recebido DECIMAL(12,2),
        data_Recebimento DATE,
		fechada BIT,
);
CREATE TABLE Clientes(
		id INT PRIMARY KEY IDENTITY(1,1),
        nome VARCHAR(100),
		cpf VARCHAR(14),
		rg VARCHAR(12),
		data_Nascimento DATE
);