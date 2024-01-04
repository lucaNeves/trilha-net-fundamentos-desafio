using System.Text.RegularExpressions;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<string> veiculos = new List<string>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public void AdicionarVeiculo()
        {
            // Feito!!!
            bool continuarTentando = true;
            while(continuarTentando)
            {
                Console.WriteLine("Digite a placa do veículo para estacionar:");
                string placa = Console.ReadLine().ToUpper(); //valores são postos em maiúsculas
                if(validarPlaca(placa))
                {
                    veiculos.Add(placa);
                    Console.WriteLine("Veículo adicionado.");
                    continuarTentando = false;
                }
                else
                {
                    // Caso seja uma placa inválida.
                    // pode tentar de novo ou desistir.
                    Console.WriteLine("Esta é uma placa inválida! Escolha uma opção...");
                    bool decisao = true;
                    while(decisao)
                    {
                        Console.WriteLine("1 - Tentar de novo");
                        Console.WriteLine("2 - Cancelar");
                        switch(Console.ReadLine())
                        {
                            case "1":
                                decisao = false;
                                Console.Clear();
                                break;
                            case "2":
                                decisao = false;
                                continuarTentando = false;
                                break;
                            default:
                                Console.WriteLine("Opção inválida. Escolha de novo...");
                                break;
                        }
                    }
                }
            }   
        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");

            // Feito!!!
            string placa = Console.ReadLine().ToUpper(); //valores são postos em maiúsculas
            // Verifica se o veículo existe
            if (veiculos.Any(x => x.Replace("-","").Trim().ToUpper() == placa.Replace("-","").Trim().ToUpper()))
            {
                Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");

                // Feito!!!
                int horas = Convert.ToInt32(Console.ReadLine());
                decimal valorTotal = precoInicial + precoPorHora * Convert.ToDecimal(horas); 

                // Feito!!!
                veiculos.Remove(placa);
                veiculos.Remove(placa.Replace("-",""));

                Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: R$ {valorTotal}");
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
            }
        }

        public void ListarVeiculos()
        {
            // Verifica se há veículos no estacionamento
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                //Feito!!!
                foreach(string veiculo in veiculos)
                {
                    Console.WriteLine(veiculo);
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }

        // placas serão registradas com e sem o '-'. Basta ter os valores certos.
        private bool validarPlaca(string placaDigitada)
        {
            // Valida se foi realmente digitado uma placa correta.
            bool placaCorreta = true;
            //verifica se o valor digitado é vazio, nulo ou tem apenas espaços em branco.
            if(string.IsNullOrWhiteSpace(placaDigitada))
            {
                placaCorreta = false;
            }
            else if(placaDigitada.Length > 8)
            {
                placaCorreta = false;
            }
            else
            {
                placaDigitada = placaDigitada.Replace("-","").Trim();
                // Verifica se a placa está no padrão mercosul ou no antigo.
                if(char.IsLetter(placaDigitada,4))
                {
                    // padrão mercosul, 4 letras e 3 número (LLL-NLNN)
                    Regex regraMercosul = new Regex("^[A-Z]{3}[0-9][A-Z][0-9]{2}$");
                    placaCorreta = regraMercosul.IsMatch(placaDigitada);
                }
                else
                {
                    // padrão antigo, 3 letras e 4 números (LLL-NNNN)
                    Regex regraAntiga = new Regex("^[A-Z]{3}[0-9]{4}$");
                    placaCorreta = regraAntiga.IsMatch(placaDigitada);
                }
            }
            return placaCorreta;
        }
    }
}
