﻿using System;
using System.Runtime.InteropServices;
using Syslaps.Pdv.Entity.SAT;

namespace Syslaps.Pdv.Infra.SAT
{
  public class Elgin : SatBase
  {
    [DllImport("Elgin_SAT.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern IntPtr ConsultarStatusOperacional(int sessao, string codigoDeAtivacao);

    [DllImport("Elgin_SAT.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern IntPtr EnviarDadosVenda(int sessao, string cod, string dados);

    [DllImport("Elgin_SAT.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern IntPtr CancelarUltimaVenda(int sessao, string cod, string chave, string dadoscancel);

    [DllImport("Elgin_SAT.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern IntPtr TesteFimAFim(int sessao, string cod, string dados);

    [DllImport("Elgin_SAT.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern IntPtr ConsultarSAT(int sessao);

    [DllImport("Elgin_SAT.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern IntPtr ConsultarNumeroSessao(int sessao, string cod, int sessao_a_ser_consultada);

    [DllImport("Elgin_SAT.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern IntPtr AtivarSAT(int sessao, int tipoCert, string cod_Ativacao, string cnpj, int uf);

    [DllImport("Elgin_SAT.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern IntPtr ComunicarCertificadoICPBRASIL(int sessao, string cod, string csr);

    [DllImport("Elgin_SAT.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern IntPtr ConfigurarInterfaceDeRede(int sessao, string cod, string xmlConfig);

    [DllImport("Elgin_SAT.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern IntPtr AssociarAssinatura(int sessao, string cod, string cnpj, string sign_cnpj);

    [DllImport("Elgin_SAT.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern IntPtr DesbloquearSAT(int sessao, string cod_ativacao);

    [DllImport("Elgin_SAT.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern IntPtr BloquearSAT(int sessao, string cod_ativacao);

    [DllImport("Elgin_SAT.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern IntPtr TrocarCodigoDeAtivacao(int sessao, string cod_ativacao, int opcao, string nova_senha, string conf_senha);

    [DllImport("Elgin_SAT.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern IntPtr ExtrairLogs(int sessao, string cod_ativacao);

    [DllImport("Elgin_SAT.dll", CallingConvention = CallingConvention.StdCall)]
    private static extern IntPtr AtualizarSoftwareSAT(int sessao, string cod_ativacao);

    public override GetStatusResponse GetStatus()
    {
      return new GetStatusResponse().Create(Marshal.PtrToStringAnsi(Elgin.ConsultarStatusOperacional(this.GenerateSessionCode(), this.ActivationCode)));
    }

    public override SendResponse Send(string xml)
    {
      return new SendResponse().Create(Marshal.PtrToStringAnsi(Elgin.EnviarDadosVenda(this.GenerateSessionCode(), this.ActivationCode, xml)));
    }

    public override InquireResponse Inquire(int sessionCode)
    {
      return new InquireResponse().Create(Marshal.PtrToStringAnsi(Elgin.ConsultarNumeroSessao(this.GenerateSessionCode(), this.ActivationCode, sessionCode)));
    }

    public override CancelResponse Cancel(string key, string xml)
    {
      return new CancelResponse().Create(Marshal.PtrToStringAnsi(Elgin.CancelarUltimaVenda(this.GenerateSessionCode(), this.ActivationCode, key, xml)));
    }

    public override SatResponse CheckAvailability()
    {
      return new SatResponse().Create(Marshal.PtrToStringAnsi(Elgin.ConsultarSAT(this.GenerateSessionCode())));
    }

    public override SatResponse CheckCommunication(string xml)
    {
      return (SatResponse) new CheckCommunicationResponse().Create(Marshal.PtrToStringAnsi(Elgin.TesteFimAFim(this.GenerateSessionCode(), this.ActivationCode, xml)));
    }

    public override SatResponse ConfigureLan(string xmlConfiguration)
    {
      return new SatResponse().Create(Marshal.PtrToStringAnsi(Elgin.ConfigurarInterfaceDeRede(this.GenerateSessionCode(), this.ActivationCode, xmlConfiguration)));
    }

    public override SatResponse AssignSignature(string cnpj, string signature)
    {
      return new SatResponse().Create(Marshal.PtrToStringAnsi(Elgin.AssociarAssinatura(this.GenerateSessionCode(), this.ActivationCode, cnpj, signature)));
    }

    public override SatResponse SoftwareUpdate()
    {
      return new SatResponse().Create(Marshal.PtrToStringAnsi(Elgin.AtualizarSoftwareSAT(this.GenerateSessionCode(), this.ActivationCode)));
    }

    public override SatResponse ExtractLog()
    {
      return (SatResponse) new ExtractLogResponse().Create(Marshal.PtrToStringAnsi(Elgin.ExtrairLogs(this.GenerateSessionCode(), this.ActivationCode)));
    }

    public override SatResponse Lock()
    {
      return new SatResponse().Create(Marshal.PtrToStringAnsi(Elgin.BloquearSAT(this.GenerateSessionCode(), this.ActivationCode)));
    }

    public override SatResponse Unlock()
    {
      return new SatResponse().Create(Marshal.PtrToStringAnsi(Elgin.DesbloquearSAT(this.GenerateSessionCode(), this.ActivationCode)));
    }

    public override SatResponse ChangeActivationCode(int option, string newActivationCode, string newActivationCodeConfirmation)
    {
      return new SatResponse().Create(Marshal.PtrToStringAnsi(Elgin.TrocarCodigoDeAtivacao(this.GenerateSessionCode(), this.ActivationCode, option, newActivationCode, newActivationCodeConfirmation)));
    }
  }
}
