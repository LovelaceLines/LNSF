import { AppBar, Button, Drawer, Link, Toolbar, useMediaQuery, useTheme } from "@mui/material";
import { AuthContext, RoleContext, iUser, useDrawerContext } from "../../Contexts";
import { Logout } from '@mui/icons-material';
import { Box } from '@mui/system';
import { useContext, useEffect, useState } from "react";
import { ListUlMenu } from "./Lists";
import nomelogo from '../../assets/logo_Variant4.svg';
import { NavLink } from "react-router-dom";

interface ILateralMenuProps { children: React.ReactNode; }

export const LateralMenu: React.FC<ILateralMenuProps> = ({ children }) => {
  const theme = useTheme();
  const smDown = useMediaQuery(theme.breakpoints.down('sm'));
  const [drawerVariant, setDrawerVariant] = useState<"permanent" | "temporary">("permanent");
  const { isDrawerOpen, setIsDrawerOpen, toggleDrawerOpen, drawerOptions, setDrawerOptions } = useDrawerContext();
  const [openIndex, setOpenIndex] = useState<number>(-1);
  const { getUser, logout } = useContext(AuthContext);
  const { isAdministrador, isAssistenteSocial, isDesenvolvedor, isSecretario, isVoluntario } = useContext(RoleContext);
  const [user, setUser] = useState<iUser | null>(null);

  useEffect(() => {
    const fetchUser = async () => setUser(await getUser());

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
            pathOption: '/inicio/registrodiario/cadastrar',
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
          },
          {
            pathOption: '/inicio/hospedagens/gerenciar/cadastrar',
            labelOption: 'Gerenciar',
          },
          {
            pathOption: '/inicio/hospedagens/pessoashospedadas/visualizar',
            labelOption: 'Pessoas Hospedadas',
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
            {
              pathOption: '/inicio/pessoas/gerenciar/cadastrar',
              labelOption: 'Gerenciar',
            }
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
            {
              pathOption: '/inicio/hospital/gerenciar/cadastrar',
              labelOption: 'Gerenciar',
            }
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
            {
              pathOption: '/inicio/tratamentos/gerenciar/cadastrar',
              labelOption: 'Gerenciar',
            }
          ],
        },
      )
    }

    if (isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) {
      menu.push(
        {
          index: 9,
          icon: 'groups',
          path: '/inicio/registrosocioeconomico',
          label: 'Registro Socioeconômico',
          options: [
            {
              pathOption: '/inicio/registrosocioeconomico/perfildogrupofamiliar/visualizar',
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
              pathOption: '/inicio/usuarios/visualizar',
              labelOption: 'Visualizar',
            },
            {
              pathOption: '/inicio/usuarios/gerenciar/cadastrar',
              labelOption: 'Gerenciar',
            },
          ],
        },
      )
    }

    setDrawerOptions(menu)
  }, [user]);

  useEffect(() => {
    if (smDown) {
      setDrawerVariant("temporary");
      setIsDrawerOpen(false);
    } else {
      setDrawerVariant("permanent");
      setIsDrawerOpen(true);
    }
  }, [smDown]);

  const handleLogout = () => {
    logout();
  };

  const LateralMenu = (
    <Drawer
      open={isDrawerOpen}
      variant={drawerVariant}
      onClose={toggleDrawerOpen}
    >
      <Box
        width={theme.spacing(32)}
        height='100%'
        marginBottom={1}
        display='flex'
        flexDirection='column'
        overflow='hidden'
      >
        <AppBar
          component={Toolbar}
          position='relative'
          elevation={0}
          color='transparent'
          sx={{ flexDirection: 'row', alignItems: 'center' }}
        >
          <Link component={NavLink} to='/inicio' sx={{ display: 'flex', alignItems: 'center' }}>
            <img src={nomelogo} style={{ height: '30px' }} />
          </Link>
        </AppBar>

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

        <Box marginTop='auto'>
          {/* Botão sair */}
          <Box
            component={Toolbar}
            display='flex'
            flexDirection='column'
            gap={1}
            sx={{ justifyContent: 'center' }}
          >
            <Link
              underline='hover'
              component={NavLink}
              to='/sobre'>
              Sobre
            </Link>
            <Button
              size='large'
              fullWidth
              startIcon={<Logout />}
              onClick={handleLogout}
            >
              Sair
            </Button>
          </Box>
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