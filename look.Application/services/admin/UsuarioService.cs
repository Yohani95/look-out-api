using System.Security.Cryptography;
using System.Text;
using look.Application.interfaces.admin;
using look.domain.entities.admin;
using look.domain.interfaces.admin;


namespace look.Application.services.admin
{
    public class UsuarioService : Service<Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository repository) : base(repository)
        {
            _usuarioRepository = repository;
        }

        public async Task encriptarPassword(Usuario usuario)
        {
            
            string textoEncriptado = Encriptar(usuario.UsuContraseña, "9#JwPz$T7@u&yAqK");
            usuario.UsuContraseña = textoEncriptado;
            AddAsync(usuario);
        }

        public async Task ActualizaUsuario(Usuario usuario)
        {
            string textoEncriptado = Encriptar(usuario.UsuContraseña, "9#JwPz$T7@u&yAqK");
            usuario.UsuContraseña = textoEncriptado;
            UpdateAsync(usuario);
            
        }

        public async Task<List<Usuario>> ListComplete()
        {
            return await _usuarioRepository.GetComplete();
        }

        public async Task<Usuario> Login(Usuario usuario)
        {
            string textoEncriptado = Encriptar(usuario.UsuContraseña, "9#JwPz$T7@u&yAqK");
            usuario.UsuContraseña = textoEncriptado;
            return await _usuarioRepository.Login(usuario);
        }
        
        public static string Encriptar(string texto, string clave)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(clave);
                aesAlg.IV = new byte[16]; // El vector de inicialización debe tener 16 bytes

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(texto);
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public static string Desencriptar(string textoEncriptado, string clave)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(clave);
                aesAlg.IV = new byte[16];

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(textoEncriptado)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
    
}
