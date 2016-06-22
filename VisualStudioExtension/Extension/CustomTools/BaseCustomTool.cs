using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.ComponentModel;

namespace NormalizedSystems.Net.CustomTools
{
    [ComVisible(true)]
    public abstract class BaseCustomTool : IVsSingleFileGenerator, IObjectWithSite, IEnumerable<string>
    {
        private object site;
        private ServiceProvider serviceProvider = null;
        
        private string bstrInputFileContents;
        private string wszInputFilePath;
        private EnvDTE.Project project;

        private List<string> newFileNames;

        protected EnvDTE.Project Project
        {
            get
            {
                return project;
            }
        }

        protected string InputFileContents
        {
            get
            {
                return bstrInputFileContents;
            }
        }

        protected string InputFilePath
        {
            get
            {
                return wszInputFilePath;
            }
        }

        private ServiceProvider SiteServiceProvider
        {
            get
            {
                if (serviceProvider == null)
                {
                    Microsoft.VisualStudio.OLE.Interop.IServiceProvider oleServiceProvider = site as Microsoft.VisualStudio.OLE.Interop.IServiceProvider;
                    serviceProvider = new ServiceProvider(oleServiceProvider);
                }
                return serviceProvider;
            }
        }

        public int DefaultExtension(out string pbstrDefaultExtension)
        {
            try
            {
                pbstrDefaultExtension = Strings.DefaultExtension;
                return VSConstants.S_OK;
            }
            catch (Exception e)
            {
                Debug.WriteLine(Strings.GetDefaultExtensionFailed);
                Debug.WriteLine(string.Format("{0} : {1}", e.GetType().Name, e.ToString()));
                pbstrDefaultExtension = string.Empty;
                return VSConstants.E_FAIL;
            }
        }

        public BaseCustomTool()
        {
            var dte = (EnvDTE.DTE)Package.GetGlobalService(typeof(EnvDTE.DTE));
            Array ary = (Array)dte.ActiveSolutionProjects;
            if (ary.Length > 0)
            {
                project = (EnvDTE.Project)ary.GetValue(0);

            }
            newFileNames = new List<string>();
        }

        public abstract string generate(string input, string ns, string inputfile, IVsGeneratorProgress pGenerateProgress);

        public int Generate(string wszInputFilePath, string bstrInputFileContents, string wszDefaultNamespace, IntPtr[] rgbOutputFileContents, out uint pcbOutput, IVsGeneratorProgress pGenerateProgress)
        {
            try
            {
                var input = bstrInputFileContents;
                var inputfile = wszInputFilePath;
                var ns = wszDefaultNamespace;

                var output = generate(input, ns, inputfile, pGenerateProgress);
                
                if (string.IsNullOrWhiteSpace(output))
                {
                    // This signals that GenerateCode() has failed. Tasklist items have been put up in GenerateCode()
                    rgbOutputFileContents = null;
                    pcbOutput = 0;

                    // Return E_FAIL to inform Visual Studio that the generator has failed (so that no file gets generated)
                    return VSConstants.E_FAIL;
                }
                else
                {
                    // The contract between IVsSingleFileGenerator implementors and consumers is that 
                    // any output returned from IVsSingleFileGenerator.Generate() is returned through  
                    // memory allocated via CoTaskMemAlloc(). Therefore, we have to convert the 
                    // byte[] array returned from GenerateCode() into an unmanaged blob.  

                    var bytes = Encoding.UTF8.GetBytes(output);

                    var outputLength = bytes.Length;
                    rgbOutputFileContents[0] = Marshal.AllocCoTaskMem(outputLength);
                    Marshal.Copy(bytes, 0, rgbOutputFileContents[0], outputLength);
                    pcbOutput = (uint)outputLength;

                    return VSConstants.S_OK;
                }
            }
            catch (Exception ex)
            {
                pGenerateProgress.GeneratorError(0, 0, string.Format("Error processing file: {0} - {1}\r\n{2}", ex.GetType().Name, ex.Message, ex.StackTrace), 0xFFFFFFFF, 0xFFFFFFFF);

                // This signals that GenerateCode() has failed. Tasklist items have been put up in GenerateCode()
                rgbOutputFileContents = null;
                pcbOutput = 0;

                // Return E_FAIL to inform Visual Studio that the generator has failed (so that no file gets generated)
                return VSConstants.E_FAIL;
            }
        }

        public void GetSite(ref Guid riid, out IntPtr ppvSite)
        {
            if (this.site == null)
            {
                throw new Win32Exception(-2147467259);
            }

            IntPtr objectPointer = Marshal.GetIUnknownForObject(this.site);

            try
            {
                Marshal.QueryInterface(objectPointer, ref riid, out ppvSite);
                if (ppvSite == IntPtr.Zero)
                {
                    throw new Win32Exception(-2147467262);
                }
            }
            finally
            {
                if (objectPointer != IntPtr.Zero)
                {
                    Marshal.Release(objectPointer);
                    objectPointer = IntPtr.Zero;
                }
            }
        }

        public void SetSite(object pUnkSite)
        {
            this.site = pUnkSite;
        }

        public virtual IEnumerator<string> GetEnumerator()
        {
            return this.newFileNames.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
} 