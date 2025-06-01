# Handling .NET HTTPS Self Certification

## 1. (Optional but recommended) Install/Update necessary packages
```
sudo pacman -Syu --needed ca-certificates nss
```

## 2. Export the dev certificate
```
mkdir -p ${HOME}/.aspnet/https
dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetcore-localhost.pem --format PEM
```

## 3. Add to system trust (Option A: copy then update)
```
sudo cp ${HOME}/.aspnet/https/aspnetcore-localhost.pem /etc/ca-certificates/trust-source/anchors/aspnetcore-localhost.crt
sudo update-ca-trust
```

## OR 3. (Option B: use trust utility directly)
```
sudo trust anchor --store ${HOME}/.aspnet/https/aspnetcore-localhost.pem
sudo update-ca-trust # or sudo trust extract-compat
```

## 4. Verify
```
dotnet dev-certs https --check --verbose
```

## 5. (If needed for browser trust, after OpenSSL is fixed)
Ensure nss is installed and then try:
```
dotnet dev-certs https --trust
```

# Set the environment varibles

Choose your shell's configuration file:

- If you use bash (common default), it's ~/.bashrc.
- If you use zsh, it's ~/.zshrc.
- If you use fish, it's ~/.config/fish/config.fish.

Add the export command to the file. Make sure the path to your .pem file is correct.

For bash or zsh:
```Bash
echo 'export SSL_CERT_FILE=${HOME}/.aspnet/https/aspnetcore-localhost.pem' >> ~/.bashrc
# Or if using zsh, replace ~/.bashrc with ~/.zshrc
```

For fish shell:
```Bash
echo 'set -gx SSL_CERT_FILE ${HOME}/.aspnet/https/aspnetcore-localhost.pem' >> ~/.config/fish/config.fish
```

Apply the changes to your current terminal session:

For bash:
```Bash
source ~/.bashrc
```

For zsh:
```Bash
source ~/.zshrc
```
For fish, changes are often picked up automatically in new shells, or you can start a new fish session.