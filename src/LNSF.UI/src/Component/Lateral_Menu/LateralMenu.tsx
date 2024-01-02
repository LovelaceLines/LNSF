import { Drawer, useMediaQuery, useTheme } from "@mui/material";
import { AuthContext, iUser, useDrawerContext } from "../../Contexts";
import { Box } from '@mui/system';
import { useContext, useEffect, useState } from "react";
import { ListUlMenu } from "./Lists";
import nomelogo from '../../assets/logo_Variant4.svg';

interface ILateralMenuProps { children: React.ReactNode; }

export const LateralMenu: React.FC<ILateralMenuProps> = ({ children }) => {
  const theme = useTheme();
  const smDown = useMediaQuery(theme.breakpoints.down('sm'));
  const [drawerVariant, setDrawerVariant] = useState<"permanent" | "temporary">("permanent");
  const { isDrawerOpen, setIsDrawerOpen, toggleDrawerOpen, drawerOptions, setDrawerOptions } = useDrawerContext();
  const [openIndex, setOpenIndex] = useState<number>(-1);
  const { getUser } = useContext(AuthContext);
  const [user, setUser] = useState<iUser | null>(null);

  useEffect(() => {
    console.log("useEffect LateralMenu")
    const fetchUser = async () => {
      try {
        setUser(await getUser());
      } catch (error) {
        console.log("error useEffect LateralMenu: ", error);
      }
    };

    fetchUser();
  }, []);

  const handleClick = (index: number) => {
    setOpenIndex(index === openIndex ? -1 : index);
  };

  useEffect(() => {
    const menu = [
      {
        index: 1,
        icon: 'home',
        path: '/inicio',
        label: 'Início',
        options: [
          {
            pathOption: '/inicio',
            labelOption: 'Início',
          },
        ],
      },
      {
        index: 4,
        icon: 'description',
        path: '/inicio/registrodiario',
        label: 'Registro de saídas',
        options: [
          {
            pathOption: '/inicio/registrodiario/visualizar',
            labelOption: 'Visualizar',
          },
          {
            pathOption: '/inicio/registrodiario/adicionar',
            labelOption: 'Adicionar',
          }
        ],
      },
      {
        index: 8,
        icon: 'bed',
        path: '/inicio/hospedagem',
        label: 'Hospedagens',
        options: [
          {
            pathOption: '/inicio/hospedagens/visualizar',
            labelOption: 'Visualizar',
          }
        ],
      },
      {
        index: 2,
        icon: 'apartment',
        path: '/inicio/apartamento',
        label: 'Apartamentos',
        options: [
          {
            pathOption: '/inicio/apartamentos/visualizar',
            labelOption: 'Visualizar',
          }
        ],
      },
    ];

    const isDesenvolvedor = !!user && user.roles.includes("Desenvolvedor");
    const isAdministrador = !!user && user.roles.includes("Administrador");
    const isSecretario = !!user && user.roles.includes("Secretário");
    const isAssistenteSocial = !!user && user.roles.includes("Assistente Social");
    const isVoluntario = !!user && user.roles.includes("Voluntário");

    if (isDesenvolvedor || isAdministrador || isSecretario) {
      menu[3].options.push(
        {
          pathOption: '/inicio/apartamentos/gerenciar',
          labelOption: 'Gerenciar',
        },
      )
    }

    if (isDesenvolvedor || isAdministrador || isSecretario) {
      menu.push(
        {
          index: 3,
          icon: 'people',
          path: '/inicio/pessoas',
          label: 'Pessoas',
          options: [
            {
              pathOption: '/inicio/pessoas/visualizar',
              labelOption: 'Visualizar',
            },
          ],
        },
      )
    }

    if (isDesenvolvedor || isAdministrador || isSecretario) {
      menu.push(
        {
          index: 5,
          icon: 'domain',
          path: '/inicio/hospital',
          label: 'Hospitais',
          options: [
            {
              pathOption: '/inicio/hospital/visualizar',
              labelOption: 'Visualizar',
            },
          ],
        },
      )
    }

    if (isDesenvolvedor || isAdministrador || isSecretario) {
      menu.push(
        {
          index: 6,
          icon: 'vaccines',
          path: '/inicio/tratamentos',
          label: 'Tratamentos',
          options: [
            {
              pathOption: '/inicio/tratamentos/visualizar',
              labelOption: 'Visualizar',
            },
          ],
        },
      )
    }

    if (isDesenvolvedor || isAdministrador) {
      menu.push(
        {
          index: 7,
          icon: 'settings',
          path: '/inicio/usuarios',
          label: 'Usuários do sistema',
          options: [
            {
              pathOption: '/inicio/usuarios/gerenciar',
              labelOption: 'Gerenciar',
            },
          ],
        },
      )
    }

    setDrawerOptions(menu)
  }, [setDrawerOptions, user]);

  useEffect(() => {
    if (smDown) {
      setDrawerVariant("temporary");
      setIsDrawerOpen(false);
    } else {
      setDrawerVariant("permanent");
      setIsDrawerOpen(true);
    }
  }, [setIsDrawerOpen, smDown]);

  const LateralMenu = (
    <Drawer
      open={isDrawerOpen}
      variant={drawerVariant}
      onClose={toggleDrawerOpen}
    >
      <Box
        id="LateralMenu"
        width={theme.spacing(32)}
        height='100%'
        display='flex'
        flexDirection='column'
      >
        <Box
          width='100%'
          padding={theme.spacing(3)}
          display='flex'
          alignItems='center'
          gap={theme.spacing(1)}
        >
          <img src={nomelogo} />
        </Box>

        {/* Itens do menu lateral */}
        <Box>
          {!!user && drawerOptions.map(drawerOption => (
            <ListUlMenu
              key={drawerOption.path}
              index={drawerOption.index}
              icon={drawerOption.icon}
              maintitle={drawerOption.label}
              subItems={drawerOption.options}
              openIndex={openIndex}
              onClickCollapse={handleClick}
            />
          ))}
        </Box>
      </Box>
    </Drawer>
  );

  return (
    <Box
      display='flex'
    >
      {LateralMenu}
      <Box
        width='100%'
        marginLeft={(isDrawerOpen && !smDown) ? 32 : 0}
      >
        {children}
      </Box>
    </Box>
  );
};