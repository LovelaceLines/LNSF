namespace LNSF.Domain.Enums;

public enum SocialProgram
{
    Other,
    AuxilioBrasil,
    BeneficioDePrestacaoContinuada,
    ProgramaMaisInfancia,
}

public enum DomicileType
{
    Own,
    Propria,
    Alugada,
    Cedida,
    Ocupada,
}

public enum MaterialExternalWallsDomicile
{
    Other,
    AlvenariaOuMadeiraAparelhada,
    MadeiraAproveitadaTaipaOuOutrosMateriaisPrecarios,
}

public enum AccessElectricalEnergy
{
    Other,
    SimComMedidorProprio,
    SimComMedidorCompartilhado,
    SimSemMedidor,
    NaoPossuiEnergiaEletricaNoDomicilio,
}

public enum WayWaterSupply
{
    Other,
    RedeGeralDeDistribuicao,
    PocoOuNascente,
    CisternaDeCaptacaoDeAguasDeChuva,
    CarroPipa,
}

public enum SanitaryDrainage
{
    Other,
    RedeColetoraDeEsgotoOuPluvial,
    FossaSeptica,
    FossaRudimentar,
    DiretoParaValaRioLagoOuMar,
    DomicilioSemBanheiro,
}

public enum GarbageCollection
{
    Other,
    SimColetaDireta,
    SimColetaIndireta,
    NaoPossuiColeta,
}

public enum DomicileHasAccessibility
{
    Other,
    SimTantoNosEspacosInternosComoNaComunicacaoComARua,
    SimApenasNosEspacosInternosMaisPossuiBarreirasNaComunicacaoComARua,
    NaoPossuiCondicoesDeAcessibilidade,
}

public enum AccessToUnit
{
    Other,
    PorDemandaEspontanea,
    EmDecorrenciaDeBuscaAtivaRealizadaPelaEquipeDaUnidade,
    EmDecorrenciaDeEncaminhamentoRealizadoPorOutrosServicosUnidadesDaProtecaoSocialBasica,
    EmDecorrenciaDeEncaminhamentoRealizadoPorOutrosServicosUnidadesDaProtecaoSocialEspecial,
    EmDecorrenciaDeEncaminhamentoRealizadoPelaAreaDeSaude,
    EmDecorrenciaDeEncaminhamentoRealizadoOutrasPoliticasSetoriais,
    EmDecorrenciaDeEncaminhamentoRealizadoPeloSistemaDeGarantiaDeDireitos,
}
