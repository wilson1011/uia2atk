%define		debug_package %{nil}

#
# spec file for package UiaAtkBridge
#

Name:           uiadbuscorebridge
Version:        1.9.0
Release:        1
License:        MIT
Group:          System/Libraries
URL:		http://www.mono-project.com/Accessibility
Source0:        http://ftp.novell.com/pub/mono/sources/uiadbuscorebridge/%{name}-%{version}.tar.bz2
BuildRoot:      %{_tmppath}/%{name}-%{version}-%{release}-root-%(%{__id_u} -n)
Requires:	mono-core >= 2.4 gtk-sharp2 >= 2.12.8
Requires:	mono-uia mono-winfxcore at-spi
BuildRequires:	mono-devel gtk-sharp2 ndesk-dbus mono-uia
#BuildRequires:	mono-devel >= 2.4 gtk-sharp2 >= 2.12.8 
#BuildRequires:	mono-uia mono-winfxcore atk-devel gtk2-devel

Summary:        Bridge between UIA providers and Dbus

%description
Bridge between UIA providers and Dbus

%package -n uiadbuscorebridge-devel
License:        MIT
Summary:        mono-uia devel package
Group:          System/Libraries
Requires:       uiadbuscorebridge == %{version}-%{release}

%description
Bridge between UIA providers and Dbus

%prep
%setup -q

%build
%configure --disable-tests
make %{?_smp_mflags}

%install
rm -rf %{buildroot}
make DESTDIR=%{buildroot} install


%clean
rm -rf %{buildroot}

%files
%defattr(-,root,root)
%doc COPYING README NEWS
%dir %_libdir/uiadbuscorebridge
%_libdir/uiadbuscorebridge/DbusCore.dll*
%_libdir/uiadbuscorebridge/UiaDbusCoreBridge.dll*
%_libdir/mono/accessibility/DbusCore.dll
%_libdir/mono/gac/DbusCore
%_libdir/mono/gac/UiaDbusCoreBridge

%files -n uiadbuscorebridge-devel
%defattr(-,root,root)
%_libdir/pkgconfig/*.pc


%post

%postun

%changelog
* Thu Apr 09 2009 <sshaw@decriptor.com> - 1.9.0-1
- packaged UiaDbusCoreBridge version 1.9.0 using the buildservice spec file wizard