export interface CampaingDTO {
    /** Identificador único da campanha */
    id: number;
  
    /** Nome da campanha */
    name: string;
  
    /** Descrição detalhada */
    message: string;
  
    /** Orçamento da campanha */
    budget: number;
  
    /** Data de início da campanha */
    InitialDate: string;
  
    /** Tipo da campanha */
    campaingType: string;

    /** Data de término da campanha */
    finalDate: string;
    
    /** Público-alvo da campanha */
    targetPublic: string;

    /** Status da campanha */
    campaingStatus: string;
  }
  